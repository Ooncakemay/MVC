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
        public string Id { get; set; }
        public string SpriteIndex { get; set; }
        public bool IsJoker { get; set; }
        public State State { get; set; }
        
        
        public CardData(string id,string spriteIndex,bool isJoker)
        {
            Id = id;
            SpriteIndex = spriteIndex;
            IsJoker = isJoker;
            State = State.Back;
        }

    }
    
}