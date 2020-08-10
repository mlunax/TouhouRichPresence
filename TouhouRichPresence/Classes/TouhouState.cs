namespace TouhouRichPresence.Classes
{
    public class TouhouState
    {
        public TouhouState(string name, string difficulty, CharacterAndShotType characterAndShotType, string stage, bool playing, bool paused)
        {
            Name = name;
            Difficulty = difficulty;
            CharacterAndShotType = characterAndShotType;
            Stage = stage;
            Playing = playing;
            Paused = paused;
        }

        public string Name { get; }
        public string Difficulty { get; }
        public CharacterAndShotType CharacterAndShotType { get; }
        public string Stage { get; }
        public bool Playing { get; }
        public bool Paused { get; }
    }
}