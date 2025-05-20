using System.Collections.Generic;

namespace Work3
{
    public interface ICardModel
    {
        IReadOnlyList<CardData> GetAllCards();
        IReadOnlyList<string> GetAllFrontCardsId();
        void FlipCard(string id);
        bool CheckMatch(string lastClickedCardId, string id);
        void SetCardMatch(string id);
        bool IsAllMatched();
        bool IsJoker(string id);
    }
}