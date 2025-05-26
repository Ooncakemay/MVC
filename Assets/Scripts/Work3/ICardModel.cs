using System.Collections.Generic;

namespace Work3
{
    public interface ICardModel
    {
        IReadOnlyList<CardData> GetAllCards();
        IEnumerable<int> GetAllFrontCardsId();
        
        void FlipCard(int id);
        void SetCardMatch(params int[] ids);
        bool CheckMatch(int id, int lastClickedCardId);
        bool IsAllMatched();
        bool IsJoker(int id);
        bool IsFront(int id);
    }
}