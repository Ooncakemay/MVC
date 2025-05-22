using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Work3
{
    public class PanelView: MonoBehaviour,IPanelView
    {
        
        //Card 
        [SerializeField] private Sprite cardBack;
        [SerializeField] private Sprite cardJoker;
        [SerializeField] private List<Sprite> cardSprites;
        [SerializeField] private List<Card> cards;
        
        private Dictionary<string,Card> cardDictionary = new();
        
        // background
        [SerializeField] private Sprite[] backgroundPictures;
        private const int backgroundTypeCount = 2;
        [SerializeField] private Image background;
        
        // mask
        [SerializeField] private GameObject mask;
        [SerializeField] private Text systemMsg;
        
        private Color transparent = new(1, 1, 1, 0);
        
        private ICardGameController  cardGameController;
        

        void Start () 
        {
            Init();
            cardGameController = new CardGameController(this);
        }
        
        
        private void Init()
        {
            
            AssignRandomBackground();
            systemMsg.color = transparent;
            mask.SetActive(false);
        }
        
        
        private void AssignRandomBackground()
        {
            var index = Random.Range(0, backgroundTypeCount);
            background.sprite = backgroundPictures[index];
        }
        
        public void Completed()
        {
            StartCoroutine(ShowGameCompleted());
        }

     

        public void DelayShowCardBack(string id)
        {
            if (cardDictionary.ContainsKey(id))
            {
                StartCoroutine(WaitForDelayShowCardBackThenReset(id));

            }

        }
        
        private IEnumerator WaitForDelayShowCardBackThenReset(string id)
        {
            yield return StartCoroutine(cardDictionary[id].DelayThenResetBack());
            cardGameController.ResetCardClickFlag();
        }

        public void ShowCardFront(string id)
        {
            if (cardDictionary.ContainsKey(id))
            {
                cardDictionary[id].ShowCardFront();
            }
        }

        private IEnumerator ShowGameCompleted()
        {
            mask.SetActive(true);
            systemMsg.text = "Congratulations!";
            var alpha = 0.05f;
            while(alpha < 1)
            {
                alpha += 0.05f;
                systemMsg.color = new Color(1, 1, 1, alpha);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(3f);
        }
        
        
        public void InitCard(IEnumerable<CardData> cardDatas)
        {
            foreach (var data in cardDatas)
            {
                var index = Convert.ToInt32(data.Id);
                var card = cards[index];
                
                if (data.SpriteIndex == "joker")
                {
                    InitJockerCard(card, data);
                }
                else
                {
                    InitNormalCard(data, card);
                }
                cardDictionary.Add(data.Id,card);
                
            }
         
        }

        private void InitJockerCard(Card card, CardData data)
        {
            card.Init(
                cardJoker,
                cardBack,
                () => cardGameController.ClickCard(data.Id));
        }

        private void InitNormalCard(CardData data, Card card)
        {
            var cardSpriteIndex = Convert.ToInt32(data.SpriteIndex);
            card.Init(
                cardSprites[cardSpriteIndex], 
                cardBack,
                () => cardGameController.ClickCard(data.Id));
        }
    }
}