using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace TouhouRichPresence.Classes
{
    public class TouhouRpcClient : IDisposable
    {
        private bool disposedValue;

        private readonly DiscordRpcClient client;
        private readonly TouhouGame touhou;
        private readonly Timer timer;

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

            timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
            timer.Elapsed += UpdatePresence;

            client.Initialize();
            timer.Start();
        }

        public void UpdatePresence(object sender, ElapsedEventArgs e)
        {
            var th = touhou.GetState;
            client.SetPresence(new RichPresence()
            {
                Details = $"{(th.Playing ? th.Stage : "Menu")}",
                State = $"{(th.Playing ? (th.Paused ? $"{th.Difficulty} (Paused)" : $"{th.Difficulty}") : "Idle")}",

                Assets = new Assets()
                {
                    LargeImageKey = "icon",
                    LargeImageText = th.Name,
                    SmallImageKey = th.CharacterAndShotType.Character.ToLowerInvariant(),
                    SmallImageText = $"{th.CharacterAndShotType.Character} {th.CharacterAndShotType.ShotType}"
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
