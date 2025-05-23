﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Work3
{
    public class CardModel : ICardModel
    {
        private Dictionary<int, CardData> cards = new();
        private Random random = new();
        private const int TotalCard = 9;

        public CardModel()
        {
            AssignCards();
        }

        private void AssignCards()
        {
            AssignCardContent(GetUnorderedNumbers());
        }

        private void AssignCardContent(IReadOnlyList<int> unorderedNums)
        {
            var contents = GetCardContents();

            var id = unorderedNums[0];
            cards.Add(id, new CardData(id, contents[0], true));

            for (var index = 1; index < contents.Length; index++)
            {
                id = unorderedNums[index];
                cards.Add(id, new CardData(id, contents[index], false));
            }
        }

        private List<int> GetUnorderedNumbers()
        {
            var nums = Nums();
            var unorderedNums = new List<int>();

            for (var i = 0; i < TotalCard; i++)
            {
                var index = UnityEngine.Random.Range(0, nums.Count);
                unorderedNums.Add(nums[index]);
                nums.Remove(nums[index]);
            }

            return unorderedNums;
        }

        private static List<int> Nums()
        {
            var nums = new List<int>();

            for (var i = 0; i < TotalCard; i++)
            {
                nums.Add(i);
            }

            return nums;
        }


        /// <summary>
        ///  產生不包含鬼牌的卡片
        /// </summary>
        /// <returns></returns>
        private List<int> PickFourRandomCardContents()
        {
            var newSprite = new List<int>();
            var unrepeatedNums = new List<int>();
            var cardPairs = 0;

            // 只需要4類
            const int targetScore = 4;
            // 但是有六類卡片
            const int cardTypeCount = 6;

            while (cardPairs < targetScore)
            {
                var randomCardType = random.Next(0, cardTypeCount);

                if (unrepeatedNums.Contains(randomCardType) is false)
                {
                    cardPairs++;
                    unrepeatedNums.Add(randomCardType);
                    newSprite.Add(randomCardType);
                }
            }

            return newSprite;
        }


        private int[] GetCardContents()
        {
            var randomPickedContents = PickFourRandomCardContents();

            var contents = new int[TotalCard];

            const int jokerType = 6;
            contents[0] = jokerType;
            
            const int pairCount = 4; // 抽出的卡片配對數量
            for (var index = 0; index < pairCount; index++)
            {
                var first = index * 2 + 1;
                var second = first +1;
                
                contents[first] = randomPickedContents[index];
                contents[second] = randomPickedContents[index];
            }

            return contents;
        }


        public IReadOnlyList<CardData> GetAllCards()
        {
            return cards.Values.Select(card => card.Clone()).ToList();
        }

        public void FlipCard(int id)
        {
            GetCard(id).State = GetCard(id).State == State.Back ? State.Front : State.Back;
        }

        public bool CheckMatch(int lastClickedCardId, int id)
        {
            var lastCard = GetCard(lastClickedCardId);
            var currentCard = GetCard(id);

            return lastCard.SpriteType == currentCard.SpriteType;
        }

        public void SetCardMatch(int id)
        {
            GetCard(id).State = State.Match;
        }

        public bool IsAllMatched()
        {
            return cards.Values.All(card => card.IsJoker || card.State == State.Match);
        }

        public bool IsJoker(int id)
        {
            return GetCard(id).IsJoker;
        }
        
        public bool IsFront(int id)
        {
            return GetCard(id).State == State.Front;
        }

        public IEnumerable<int> GetAllFrontCardsId()
        {
            return cards.Values.Select(card => card.Id);
        }

        private CardData GetCard(int id)
        { 
            var data = cards.GetValueOrDefault(id);

            return data ?? CardData.CreateDefault();
        }
    }
}