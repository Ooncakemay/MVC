using System;
using System.Collections.Generic;
using UnityEngine;

namespace Work3
{
    public class EnterPoint:MonoBehaviour
    {
        [SerializeField] private Sprite cardBack;
        [SerializeField] private Sprite cardJoker;
        [SerializeField] private List<Sprite> cardSprites;
        [SerializeField] private List<CardView> cardViews;
        [SerializeField] private PanelView panelView;

        // 資料
        private ICardRepository _cardRepository;
        // 控制器
        private ICardGameController _cardGameController;

        public void Start()
        {
            _cardRepository = new CardRepository();
            _cardGameController = new CardGameController(_cardRepository);
            ConstructCardView();
            panelView.Construct(_cardGameController);
            
        }

        // 在這裡設定CardView顯示
        private void ConstructCardView()
        {
            var cards = _cardRepository.GetAllCards();

            for (var index = 0; index < cards.Count; index++)
            {
                var cardData = cards[index];
                var cardView = cardViews[index];

                if (cardData.SpriteIndex == "joker")
                {
                    ConstructJokerCard(cardView, cardData);
                }
                else
                {
                    ConstructNormalCard(cardView, cardData);
                }
                
            }
            
         
        }

        private void ConstructJokerCard(CardView cardView, CardData cardData)
        {
            cardView.Construct(cardData.Id,
                cardJoker,
                cardBack,
                _cardGameController);
        }

        private void ConstructNormalCard(CardView cardView,CardData cardData)
        {
            var spriteIndex = Convert.ToInt32(cardData.SpriteIndex);
            cardView.Construct(cardData.Id,
                cardSprites[spriteIndex],
                cardBack,
                _cardGameController);
        }
    }
}