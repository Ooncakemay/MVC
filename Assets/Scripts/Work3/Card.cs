using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Work3
{
    public class Card : MonoBehaviour
    {
        private Sprite front;
        private Sprite back;

        [SerializeField] private Image image;
        [SerializeField] private Button button;
        
        public void Init(Sprite front, Sprite back, UnityAction flipCard)
        {
            this.front = front;
            this.back = back;
            image.sprite = back;
            button.onClick.AddListener(flipCard);
        }

        public void ShowCardFront()
        {
            image.sprite = front;
        }

        public IEnumerator DelayThenResetBack()
        {
            yield return new WaitForSeconds(1);
            image.sprite = back;
        }
    }
}