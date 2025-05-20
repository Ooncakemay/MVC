using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Work3
{
    public class CardView: MonoBehaviour,ICardView
    {
        private string id;
        private Sprite front;
        private Sprite back;

        [SerializeField] private Image image;
        [SerializeField] private Button button;

        private ICardGameController _cardGameController;
        public void Construct( string id ,Sprite front,Sprite back ,ICardGameController cardGameController)
        {
            this.id = id;
            this.front = front;
            this.back = back;
            _cardGameController = cardGameController;
            cardGameController.RegisterCardView(this.id,this);
            button.onClick.AddListener(() => cardGameController.ClickCard(this.id) );
            ShowCardBack();
        }
        private void ShowCardBack()
        {
            image.sprite = back;
        }
        
        public void DelayShowCardBack()
        {
            StartCoroutine(DelayThenResetBack());
        }
        public void ShowCardFront()
        {
            image.sprite = front;

        }
        
        
        private IEnumerator DelayThenResetBack()
        {
            yield return new WaitForSeconds(1); 
            image.sprite = back;
            _cardGameController.ResetCardClickFlag();
        }
        
    }
}