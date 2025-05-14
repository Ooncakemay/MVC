using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Work3
{
    public class CardView: MonoBehaviour,ICardView
    {
        private string _id;
        private Sprite _front;
        private Sprite _back;

        [SerializeField] private Image image;
        [SerializeField] private Button button;

        private ICardGameController _cardGameController;
        public void Construct( string id ,Sprite front,Sprite back ,ICardGameController cardGameController)
        {
            _id = id;
            _front = front;
            _back = back;
            _cardGameController = cardGameController;
            cardGameController.RegisterCardView(_id,this);
            button.onClick.AddListener(() => cardGameController.ClickCard(_id) );
            ShowCardBack();
        }
        public void ShowCardBack()
        {
            image.sprite = _back;
        }
        
        public void DelayShowCardBack()
        {
            StartCoroutine(DelayThenResetBack());
        }
        public void ShowCardFront()
        {
            image.sprite = _front;

        }
        
        
        private IEnumerator DelayThenResetBack()
        {
            yield return new WaitForSeconds(1); 
            image.sprite = _back;
            _cardGameController.ResetCardClickFlag();
        }
        
    }
}