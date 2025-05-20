using UnityEngine.Events;

namespace Work3
{
    public interface ICardGameController
    {
        void RegisterCardView(string id, ICardView cardView);
        void RegisterPanelView(IPanelView panelView);
        void ClickCard(string id);

        void ResetCardClickFlag();

    }
}