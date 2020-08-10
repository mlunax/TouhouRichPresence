using ProcessMemoryUtilities.Managed;
using ProcessMemoryUtilities.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TouhouRichPresence.Classes
{
    public abstract class TouhouGame : IDisposable
    {
        private bool disposedValue;

        public Process TouhouProcess { get; }
        protected IntPtr TouhouHandle { get; }

        public TouhouGame(Process touhouProcess)
        {
            TouhouProcess = touhouProcess;
            TouhouHandle = NativeWrapper.OpenProcess(ProcessAccessFlags.Read, touhouProcess.Id);
            TouhouProcess.EnableRaisingEvents = true;
        }


        public virtual string GetClientId { get; }

        public virtual string GetName { get; }
        public virtual string GetDifficulty { get; }
        public virtual CharacterAndShotType GetCharacterAndShotType { get; }
        public virtual string GetStage { get; }
        public virtual bool IsPlaying { get; }
        public virtual bool IsPaused { get; }

        public TouhouState GetState
        {
            get
            {
                var state = new TouhouState(GetName, GetDifficulty, GetCharacterAndShotType, GetStage, IsPlaying, IsPaused);
                return state;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                NativeWrapper.CloseHandle(TouhouHandle);
                disposedValue = true;
            }
        }
        ~TouhouGame()
        {
            Dispose(disposing: false);
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
