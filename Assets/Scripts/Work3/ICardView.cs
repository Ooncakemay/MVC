using System.Collections.Generic;

namespace Work3
{
    public interface ICardView
    {
        public void Completed();
        public void ShowCardBack(params int[] ids);
        public void ShowCardFront(params int[] ids);
        public void InitCard(IEnumerable<CardData> cardDatas);
    }
}