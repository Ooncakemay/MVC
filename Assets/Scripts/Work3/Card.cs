using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Work3
{
    public class Card: MonoBehaviour
    {
        private Sprite front;
        private Sprite back;

        [SerializeField] private Image image;
        [SerializeField] private Button button;


        public void Initialization(Sprite front,Sprite back,Action flipCard )
        {
            this.front = front;
            this.back = back;
            button.onClick.AddListener(flipCard.Invoke);
        
            ShowCardBack();
        }
        
        private void ShowCardBack()
        {
            image.sprite = back;
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