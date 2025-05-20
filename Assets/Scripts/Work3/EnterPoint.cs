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
        
        private ICardModel cardModel;
        private ICardGameController cardGameController;

        public void Start()
        {
            cardModel = new CardModel();
            cardGameController = new CardGameController(cardModel);
            ConstructCardView();
            panelView.Construct(cardGameController);
            
        }
        
        private void ConstructCardView()
        {
            var cards = cardModel.GetAllCards();

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
                cardGameController);
        }

        private void ConstructNormalCard(CardView cardView,CardData cardData)
        {
            var spriteIndex = Convert.ToInt32(cardData.SpriteIndex);
            cardView.Construct(cardData.Id,
                cardSprites[spriteIndex],
                cardBack,
                cardGameController);
        }
    }
}