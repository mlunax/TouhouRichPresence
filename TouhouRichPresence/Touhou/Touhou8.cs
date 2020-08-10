using ProcessMemoryUtilities.Managed;
using System;
using System.Diagnostics;
using TouhouRichPresence.Classes;

namespace TouhouRichPresence.Touhou
{
    public class Touhou8 : TouhouGame
    {
        public Touhou8(Process touhouProcess) : base(touhouProcess)
        {
        }
        
        public override string GetClientId => "742511178961518673";
        public override string GetName => "Touhou 8: Imperishable Night";
        
        public override string GetDifficulty
        {
            get
            {
                int difficulty = 0;
                NativeWrapper.ReadProcessMemory(TouhouHandle, IntPtr.Add(TouhouProcess.MainModule.BaseAddress, 0x120F538), ref difficulty);
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
                NativeWrapper.ReadProcessMemory(TouhouHandle, IntPtr.Add(TouhouProcess.MainModule.BaseAddress, 0x124CF48), ref character);
                return character switch
                {
                    0 => new CharacterAndShotType("Illusionary Barrier Team", "Reimu and Yukari"), //illusionary_barrier_team
                    1 => new CharacterAndShotType("Aria of Forbidden Magic Team", "Marisa and Alice"), //aria_of_forbidden_magic_team
                    2 => new CharacterAndShotType("Visionary Scarlet Devil Team", "Sakuya and Remillia"), //visionary_scarlet_devil_team
                    3 => new CharacterAndShotType("Netherworld Dwellers' Team", "Youmu and Yuyuko"),
                    _ => new CharacterAndShotType("Unknown", "Unknown"),
                };
            }
        }

        // IMPORTANT: Do not implement, address is not debugged
        public override string GetStage
        {
            get
            {
                int stage = 0;
                NativeWrapper.ReadProcessMemory(TouhouHandle, IntPtr.Add(TouhouProcess.MainModule.BaseAddress, 0x22F85C), ref stage);
                return $"Stage {stage}";
            }
        }

        // IMPORTANT: Do not implement, address is not debugged
        public override bool IsPlaying
        {
            get
            {
                bool isPlaying = false;
                return true;
                NativeWrapper.ReadProcessMemory(TouhouHandle, IntPtr.Add(TouhouProcess.MainModule.BaseAddress, 0x22F8C7), ref isPlaying);
                return isPlaying;
            }
        }
        
        public override bool IsPaused
        {
            get
            {
                int isPaused = 0;
                NativeWrapper.ReadProcessMemory(TouhouHandle, IntPtr.Add(TouhouProcess.MainModule.BaseAddress, 0x124D0A0), ref isPaused);
                return isPaused == 1;
            }
        }
    }
}