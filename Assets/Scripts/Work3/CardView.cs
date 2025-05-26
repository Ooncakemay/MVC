using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Work3
{
    public class CardView : MonoBehaviour, ICardView
    {
        //Card 
        [SerializeField] private Sprite cardBack;
        [SerializeField] private List<Sprite> cardSprites;
        [SerializeField] private List<Card> cards;

        private Dictionary<int, Card> cardDictionary = new();

        // background
        [SerializeField] private Sprite[] backgroundPictures;
        private const int backgroundTypeCount = 2;
        [SerializeField] private Image background;

        // mask
        [SerializeField] private GameObject mask;
        [SerializeField] private Text systemMsg;

        private Color transparent = new(1, 1, 1, 0);

        private ICardGameController cardGameController;


        void Start()
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

        private IEnumerator ShowGameCompleted()
        {
            mask.SetActive(true);
            systemMsg.text = "Congratulations!";
            var alpha = 0.05f;
            while (alpha < 1)
            {
                alpha += 0.05f;
                systemMsg.color = new Color(1, 1, 1, alpha);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(3f);
        }

        public void ShowCardBack(params int[] ids)
        {
            foreach (var id in ids)
            {
                ShowCardBack(id);
            }
        }

        private void ShowCardBack(int id)
        {
            if (cardDictionary.ContainsKey(id))
            {
                StartCoroutine(WaitForDelayShowCardBackThenReset(id));
            }
        }

        private IEnumerator WaitForDelayShowCardBackThenReset(int id)
        {
            yield return StartCoroutine(cardDictionary[id].DelayThenResetBack());
            cardGameController.ResetCardClickFlag();
        }

        public void ShowCardFront(params int[] ids)
        {
            foreach (var id in ids)
            {
                ShowCardFront(id);
            }
        }

        private void ShowCardFront(int id)
        {
            if (cardDictionary.ContainsKey(id))
            {
                cardDictionary[id].ShowCardFront();
            }
        }

        public void InitCard(IEnumerable<CardData> cardDatas)
        {
            foreach (var data in cardDatas)
            {
                var card = cards[data.Id];
                card.Init(cardSprites[data.SpriteType], cardBack, () => cardGameController.ClickCard(data.Id));
                cardDictionary.Add(data.Id, card);
            }
        }
    }
}