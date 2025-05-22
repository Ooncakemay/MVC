using System.Collections.Generic;

namespace Work3
{
    public interface IPanelView
    {
        public void Completed();
        
        public void DelayShowCardBack(string id);
        public void ShowCardFront(string id);

        public void InitCard(IEnumerable<CardData> cardDatas);


    }
}