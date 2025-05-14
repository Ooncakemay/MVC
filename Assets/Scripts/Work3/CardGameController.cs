using System;
using System.Collections.Generic;
using UnityEngine;

namespace Work3
{
 
    public class CardGameController:ICardGameController
    { 
        private readonly ICardRepository _cardRepository;
        private Dictionary<string,ICardView> _cardViews = new();
        private IPanelView _panelView;
        
        private string _lastClickedCardId = string.Empty;
        private int _cardCount;
        private const int MaxCardCount = 2;
        private bool _canClick = true;
        
        public CardGameController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        private bool CanFlip()
        {
            return _canClick;
        }
        
        public void RegisterCardView(string id, ICardView cardView)
        {
            _cardViews.Add(id, cardView);
        }

        public void ClickCard(string id)
        {
            if(!CanFlip()) 
                return;
            
            if (IsSameCard(id))
                return;

            if (IsEmptyLastCard())
                _lastClickedCardId = id;

            FlipCardToFront(id);
            
            if(_cardRepository.IsJoker(id))
            {
                _canClick = false;
                var ids =  _cardRepository.GetAllFrontCardsId();
                foreach (var cardId in ids)
                {
                    FlipCardToBack(cardId);
                }
                return;
            }
            
            if (_cardCount == MaxCardCount)
            {
                _canClick = false;
                
                var currentCardView = GetCardView(id);
                var lastCardView = GetCardView(_lastClickedCardId);
                
                var match = _cardRepository.CheckMatch(_lastClickedCardId, id);
                if (match)
                {
                    _cardRepository.SetCardMatch(_lastClickedCardId);
                    _cardRepository.SetCardMatch(id);
                    ResetCardClickFlag();
                    if (_cardRepository.IsAllMatched())
                    {
                        Congratulation();
                    }
                 
                }
                else
                {
                    _cardRepository.FlipCard(_lastClickedCardId);
                    _cardRepository.FlipCard(id);
                    currentCardView.DelayShowCardBack();
                    lastCardView.DelayShowCardBack();

                } 
            }

        }

        private void FlipCardToBack(string cardId)
        {
            GetCardView(cardId).DelayShowCardBack();
            _cardRepository.FlipCard(cardId);
        }

        private void FlipCardToFront(string id)
        {
            _cardCount++;
            _cardRepository.FlipCard(id);
            GetCardView(id).ShowCardFront();
        }

        private bool IsEmptyLastCard()
        {
            return string.IsNullOrEmpty(_lastClickedCardId);
        }

        private bool IsSameCard(string id)
        {
            return _lastClickedCardId == id && _cardCount == 1;
        }

        private void Congratulation()
        {
            _canClick = false;
            Debug.Log("Congratulations! You've matched all the cards!");
            _panelView.Completed();
        }

        public void ResetCardClickFlag()
        {
            _canClick = true;
            _cardCount = 0;
            _lastClickedCardId = "";
        }

        public void RegisterPanelView(IPanelView panelView)
        {
            _panelView = panelView;
        }

        private ICardView GetCardView(string id)
        {
            if (_cardViews.ContainsKey(id))
            {
                return _cardViews[id];
            }

            throw new Exception($"CardView with id {id} not found.");
        }
    }
}