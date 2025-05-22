using UnityEngine.Events;

namespace Work3
{
    public interface ICardGameController
    {
        void ClickCard(string id);

        void ResetCardClickFlag();

    }
}