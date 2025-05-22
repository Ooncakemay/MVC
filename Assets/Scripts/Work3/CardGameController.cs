using System;
using System.Collections.Generic;
using UnityEngine;

namespace Work3
{
 
    public class CardGameController:ICardGameController
    { 
        private readonly ICardModel cardModel;
        private IPanelView panelView;
        
        private string lastClickedCardId = string.Empty;
        private int cardCount;
        private const int MaxCardCount = 2;
        private bool canClick = true;
        
     
        public CardGameController(IPanelView panelView)
        {
            this.panelView = panelView;
            cardModel = new CardModel();
            panelView.InitCard(cardModel.GetAllCards());
        }
        

        public void ClickCard(string id)
        {
            if(!CanFlip()) 
                return;
            
            if (IsSameCard(id))
                return;

            if (IsEmptyLastCard())
                lastClickedCardId = id;

            FlipCardToFront(id);
            
            if(cardModel.IsJoker(id))
            {
                canClick = false;
                var ids =  cardModel.GetAllFrontCardsId();
                foreach (var cardId in ids)
                {
                    FlipCardToBack(cardId);
                }
                return;
            }
            
            if (cardCount == MaxCardCount)
            {
                canClick = false;
                
                var match = cardModel.CheckMatch(lastClickedCardId, id);
                if (match)
                {
                    cardModel.SetCardMatch(lastClickedCardId);
                    cardModel.SetCardMatch(id);
                    ResetCardClickFlag();
                    if (cardModel.IsAllMatched())
                    {
                        Congratulation();
                    }
                 
                }
                else
                {
                    cardModel.FlipCard(lastClickedCardId);
                    cardModel.FlipCard(id);
                    panelView.DelayShowCardBack(id);
                    panelView.DelayShowCardBack(lastClickedCardId);

                } 
            }

        }
        
        private bool CanFlip()
        {
            return canClick;
        }
        private bool IsSameCard(string id)
        {
            return lastClickedCardId == id && cardCount == 1;
        }
        
        
        private bool IsEmptyLastCard()
        {
            return string.IsNullOrEmpty(lastClickedCardId);
        }
        private void FlipCardToFront(string id)
        {
            cardCount++;
            cardModel.FlipCard(id);
            panelView.ShowCardFront(id);
        }

        private void FlipCardToBack(string cardId)
        {
            panelView.DelayShowCardBack(cardId);
            cardModel.FlipCard(cardId);
        }
        
       
        
        public void ResetCardClickFlag()
        {
            canClick = true;
            cardCount = 0;
            lastClickedCardId = "";
        }
        

        private void Congratulation()
        {
            canClick = false;
            Debug.Log("Congratulations! You've matched all the cards!");
            panelView.Completed();
        }

     


     
        
       
      
        
       

    }
}