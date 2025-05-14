using System.Collections.Generic;

namespace Work3
{
    public interface ICardRepository
    {
        IReadOnlyList<CardData> GetAllCards();
        
        bool CanFlip(string id);
        void FlipCard(string id);
        bool CheckMatch(string lastClickedCardId, string id);
        void SetCardMatch(string id);
        bool IsAllMatched();
        bool IsJoker(string id);
        
        IReadOnlyList<string> GetAllFrontCardsId();
    }
}