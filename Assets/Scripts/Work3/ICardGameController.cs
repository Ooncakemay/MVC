using UnityEngine.Events;

namespace Work3
{
    public interface ICardGameController
    {
        void RegisterCardView(string id, ICardView cardView);
        void ClickCard(string id);

        void ResetCardClickFlag();
        void RegisterPanelView(IPanelView panelView);
    }
}