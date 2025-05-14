using System;
using System.Collections.Generic;
using System.Linq;

namespace Work3
{
    public class CardRepository:ICardRepository
    {
        
        private Dictionary<string, CardData> _cards = new();
        /// 鬼牌是 100
        private readonly int _joker = 100;
        
 
        public CardRepository()
        {
            AssignCards();
     
            
        }
        
        private void AssignCards()
        {
            PickFourRandomCardContents();
            AssignCardContent(GetUnorderedNumbers());
        }
        /// <summary>
        ///  產生不包含鬼牌的卡片
        /// </summary>
        /// <returns></returns>
        private  List<int>  PickFourRandomCardContents()
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
                var rand = new Random();
                var randomCardType = rand.Next(0, cardTypeCount);
                
                if (!unrepeatedNums.Contains(randomCardType))
                {
                    cardPairs++;
                    unrepeatedNums.Add(randomCardType);
                    newSprite.Add(randomCardType);
                }
            }
            return newSprite;
        }
        
        private List<int> GetUnorderedNumbers()
        {
            List<int> nums = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                nums.Add(i);
            }
            List<int> unorderedNums = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                int index = UnityEngine.Random.Range(0, nums.Count);
                unorderedNums.Add(nums[index]);
                nums.Remove(nums[index]);
            }
            return unorderedNums;
        }
        
        private void AssignCardContent(IReadOnlyList<int> unorderedNums)
        {
            var contents = GetCardContents();

            
            for (var  index = 0; index < contents.Length ; index++)
            {
                var id = index.ToString();
                _cards.Add(id, new CardData(id ,id ,false));
            }

            for (var index = 0; index < contents.Length; index++)
            {
                var tempCard = _cards[unorderedNums[index].ToString()];
                tempCard.SpriteIndex =  contents[index] == _joker ? "joker" : contents[index].ToString();
                tempCard.IsJoker = (index == 0);
            }
        }

        private int[] GetCardContents()
        {
            var randomPickedContents = PickFourRandomCardContents();
            
            var contents = new int[9];
            
            contents[0] = _joker;
            contents[1] = randomPickedContents[0];
            contents[2] = randomPickedContents[0];
            contents[3] = randomPickedContents[1];
            contents[4] = randomPickedContents[1];
            contents[5] = randomPickedContents[2];
            contents[6] = randomPickedContents[2];
            contents[7] = randomPickedContents[3];
            contents[8] = randomPickedContents[3];

            return contents;
        }


        public IReadOnlyList<CardData> GetAllCards()
        {
            return _cards.Values.ToList();
        }
        
        public bool CanFlip(string id)
        {
            return GetCard(id).State == State.Back;
        }

        public void FlipCard(string id)
        {
            GetCard(id).State = GetCard(id).State == State.Back ? State.Front : State.Back;
        }

        public bool CheckMatch(string lastClickedCardId, string id)
        {
            var lastCard = GetCard(lastClickedCardId);
            var currentCard = GetCard(id);

            if (lastCard.SpriteIndex == currentCard.SpriteIndex)
            {
                return true;
            }

            return false;
        }

        public void SetCardMatch(string id)
        {
            GetCard(id).State = State.Match;
        }

        public bool IsAllMatched()
        {
            return _cards.Values.Where(c=> c.IsJoker == false).All(card => card.State == State.Match);
        }

        public bool IsJoker(string id)
        {
            return GetCard(id).IsJoker;
        }

        public IReadOnlyList<string> GetAllFrontCardsId()
        {
           return _cards.Values.Where(c=> c.State is State.Front or State.Match).Select(c => c.Id).ToList();
        }

        private CardData GetCard(string id)
        {
            if (_cards.ContainsKey(id))
            {
                return _cards[id];
            }

            throw new Exception("卡片不存在");
        }
    }
}