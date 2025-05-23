using System.Collections.Generic;

namespace Work3
{
    public interface ICardModel
    {
        IReadOnlyList<CardData> GetAllCards();
        IEnumerable<int> GetAllFrontCardsId();
        void FlipCard(int id);
        bool CheckMatch(int lastClickedCardId, int id);
        void SetCardMatch(int id);
        bool IsAllMatched();
        bool IsJoker(int id);
        bool IsFront(int id);
    }
}