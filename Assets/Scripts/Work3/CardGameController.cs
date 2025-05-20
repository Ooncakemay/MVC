using System;
using System.Collections.Generic;
using UnityEngine;

namespace Work3
{
 
    public class CardGameController:ICardGameController
    { 
        private readonly ICardModel cardModel;
        private Dictionary<string,ICardView> cardViews = new();
        private IPanelView panelView;
        
        private string lastClickedCardId = string.Empty;
        private int cardCount;
        private const int MaxCardCount = 2;
        private bool canClick = true;
        
     
        public CardGameController(ICardModel cardModel)
        {
            this.cardModel = cardModel;
        }
        public void RegisterPanelView(IPanelView panelView)
        {
            this.panelView = panelView;
        }
        
        public void RegisterCardView(string id, ICardView cardView)
        {
            cardViews.Add(id, cardView);
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
                
                var currentCardView = GetCardView(id);
                var lastCardView = GetCardView(lastClickedCardId);
                
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
                    currentCardView.DelayShowCardBack();
                    lastCardView.DelayShowCardBack();

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
            GetCardView(id).ShowCardFront();
        }

        private void FlipCardToBack(string cardId)
        {
            GetCardView(cardId).DelayShowCardBack();
            cardModel.FlipCard(cardId);
        }
        
        private ICardView GetCardView(string id)
        {
            if (cardViews.ContainsKey(id))
            {
                return cardViews[id];
            }

            throw new Exception($"CardView with id {id} not found.");
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