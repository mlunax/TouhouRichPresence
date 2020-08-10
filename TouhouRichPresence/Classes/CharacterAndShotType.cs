﻿namespace TouhouRichPresence.Classes
{
    public class CharacterAndShotType
    {
        public CharacterAndShotType(string character, string shotType)
        {
            Character = character;
            ShotType = shotType;
        }

        public string Character { get; }
        public string ShotType { get; }
    }
}