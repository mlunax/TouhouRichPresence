using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.Timers;

namespace TouhouRichPresence.Classes
{
    public class TouhouRpcClient : IDisposable
    {
        private bool disposedValue;

        private readonly DiscordRpcClient client;
        private readonly TouhouGame touhou;
        private readonly Timer timer;

        private DateTime? date;

        public TouhouRpcClient(TouhouGame touhou)
        {
            this.touhou = touhou;
            client = new DiscordRpcClient(touhou.GetClientId)
            {
                Logger = new ConsoleLogger(LogLevel.Warning)
            };

            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };
            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Received Update! {0}", e.Presence);
            };

            timer = new Timer(TimeSpan.FromSeconds(2).TotalMilliseconds);
            timer.Elapsed += UpdatePresence;
            client.Initialize();

            timer.Start();
        }

        public void UpdatePresence(object sender, ElapsedEventArgs e)
        {
            TouhouState th = touhou.GetState;
            if (th.Playing && date == null)
            {
                date = DateTime.UtcNow;
            }
            else if (!th.Playing)
            {
                date = null;
            }

            client.SetPresence(new RichPresence()
            {
                Details = $"{(th.Playing ? th.Stage : "Menu")}",
                State = $"{(th.Playing ? (th.Paused ? $"{th.Difficulty} (Paused)" : $"{th.Difficulty}") : "Idle")}",
                Timestamps = th.Playing ? new Timestamps(date.Value) : null,
                Assets = new Assets()
                {
                    LargeImageKey = "icon",
                    LargeImageText = th.Name,
                    SmallImageKey = th.Playing ? th.CharacterAndShotType.Character.ToLowerInvariant().Replace(' ', '_') : null,
                    SmallImageText = th.Playing ? $"{th.CharacterAndShotType.Character} {th.CharacterAndShotType.ShotType}" : null
                }
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    timer.Stop();
                    timer.Elapsed -= UpdatePresence;
                    timer.Dispose();
                    client.Dispose();
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