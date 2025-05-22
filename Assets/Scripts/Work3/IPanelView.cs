using System.Collections.Generic;

namespace Work3
{
    public interface IPanelView
    {
        public void Completed();
        public void DelayShowCardBack();
        public void ShowCardFront();
        public void InitCard(IEnumerable<CardData> cardDatas);
        
    }
}