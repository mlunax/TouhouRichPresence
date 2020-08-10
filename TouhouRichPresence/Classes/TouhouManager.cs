using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TouhouRichPresence.Touhou;

namespace TouhouRichPresence.Classes
{
    public class TouhouManager : IDisposable
    {
        private bool disposedValue;

        private TouhouGame touhou;
        private TouhouRpcClient client;

        private CancellationTokenSource cancellationTokenSource;

        public async Task StartAsync()
        {
            for (int tries = 0; tries < 20; tries++)
            {
                touhou = FindTouhouGame();
                if (touhou != null)
                {
                    break;
                }
                await Task.Delay(TimeSpan.FromSeconds(3));
            }
            if (touhou == null)
            {
                return;
            }
            cancellationTokenSource = new CancellationTokenSource();
            client = new TouhouRpcClient(touhou);
            touhou.TouhouProcess.Exited += TouhouProcessExited;

            await Task.Delay(Timeout.Infinite, cancellationTokenSource.Token);
        }

        private void TouhouProcessExited(object sender, EventArgs e)
        {
            Dispose();
        }

        private static TouhouGame FindTouhouGame()
        {
            Process touhouProcess = Process.GetProcesses().Where(process => process.ProcessName.StartsWith("th", StringComparison.InvariantCulture)).FirstOrDefault();
            return (touhouProcess?.ProcessName) switch
            {
                "th07" => new Touhou7(touhouProcess),
                _ => null,
            };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    client?.Dispose();
                    touhou?.Dispose();
                    cancellationTokenSource?.Cancel();
                    cancellationTokenSource?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}