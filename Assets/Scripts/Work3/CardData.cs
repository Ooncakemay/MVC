namespace Work3
{
    public enum State
    {
        Front,
        Back,
        Match
    }

    public class CardData
    {
        public int Id { get; set; }
        public int SpriteType { get; set; }
        public bool IsJoker { get; set; }
        public State State { get; set; }

        public CardData(int id, int spriteType, bool isJoker)
        {
            Id = id;
            SpriteType = spriteType;
            IsJoker = isJoker;
            State = State.Back;
        }
        
        public CardData Clone()
        {
            return new CardData(Id, SpriteType, IsJoker)
            {
                State = State
            };
        }

        public static CardData CreateDefault()
        {
            return new CardData(0, 0, false);
        }
    }
}