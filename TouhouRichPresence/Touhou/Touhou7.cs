using ProcessMemoryUtilities.Managed;
using System;
using System.Diagnostics;
using TouhouRichPresence.Classes;

namespace TouhouRichPresence.Touhou
{
    public class Touhou7 : TouhouGame
    {
        public Touhou7(Process touhouProcess) : base(touhouProcess)
        {
        }

        public override string GetClientId => "742342044789243995";
        public override string GetName => "Touhou 7: Perfect Cherry Blossom";

        public override string GetDifficulty
        {
            get
            {
                int difficulty = 0;
                NativeWrapper.ReadProcessMemory(TouhouHandle, IntPtr.Add(TouhouProcess.MainModule.BaseAddress, 0x226280), ref difficulty);
                return difficulty switch
                {
                    0 => "Easy",
                    1 => "Normal",
                    2 => "Hard",
                    3 => "Lunatic",
                    4 => "Extra",
                    5 => "Phantasm",
                    _ => "Unknown",
                };
            }
        }

        public override CharacterAndShotType GetCharacterAndShotType
        {
            get
            {
                byte character = 0;
                NativeWrapper.ReadProcessMemory(TouhouHandle, IntPtr.Add(TouhouProcess.MainModule.BaseAddress, 0x22F647), ref character);
                return character switch
                {
                    0 => new CharacterAndShotType("Reimu", "A"),
                    1 => new CharacterAndShotType("Reimu", "B"),
                    2 => new CharacterAndShotType("Marisa", "A"),
                    3 => new CharacterAndShotType("Marisa", "B"),
                    4 => new CharacterAndShotType("Sakuya", "A"),
                    5 => new CharacterAndShotType("Sakuya", "B"),
                    _ => new CharacterAndShotType("Unknown", "Unknown"),
                };
            }
        }

        public override string GetStage
        {
            get
            {
                int stage = 0;
                NativeWrapper.ReadProcessMemory(TouhouHandle, IntPtr.Add(TouhouProcess.MainModule.BaseAddress, 0x22F85C), ref stage);
                return $"Stage {stage}";
            }
        }

        public override bool IsPlaying
        {
            get
            {
                bool isPlaying = false;
                NativeWrapper.ReadProcessMemory(TouhouHandle, IntPtr.Add(TouhouProcess.MainModule.BaseAddress, 0x22F8C7), ref isPlaying);
                return isPlaying;
            }
        }

        public override bool IsPaused
        {
            get
            {
                int isPaused = 0;
                NativeWrapper.ReadProcessMemory(TouhouHandle, IntPtr.Add(TouhouProcess.MainModule.BaseAddress, 0xF54300), ref isPaused);
                return isPaused == 1;
            }
        }
    }
}