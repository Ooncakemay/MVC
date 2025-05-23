namespace Work3
{
    public class CardGameController : ICardGameController
    {
        private readonly ICardModel cardModel;
        private ICardView cardView;

        private int lastClickedCardId = 0;
        private int cardCount;
        private const int MaxCardCount = 2;
        private bool canClick = true;


        public CardGameController(ICardView cardView)
        {
            this.cardView = cardView;
            cardModel = new CardModel();
            cardView.InitCard(cardModel.GetAllCards());
        }


        public void ClickCard(int id)
        {
            if (!CanFlip())
                return;

            if (cardModel.IsFront(id))
                return;

            if (IsEmptyLastCard())
                lastClickedCardId = id;

            FlipCardToFront(id);
            
            if (cardModel.IsJoker(id))
            {
                canClick = false;
                var ids = cardModel.GetAllFrontCardsId();
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
                    cardView.ShowCardBack(id);
                    cardView.ShowCardBack(lastClickedCardId);
                }
            }
        }

        private bool CanFlip()
        {
            return canClick;
        }

        private bool IsEmptyLastCard()
        {
            return cardCount == 0;
        }

        private void FlipCardToFront(int id)
        {
            cardCount++;
            cardModel.FlipCard(id);
            cardView.ShowCardFront(id);
        }

        private void FlipCardToBack(int cardId)
        {
            cardView.ShowCardBack(cardId);
            cardModel.FlipCard(cardId);
        }

        public void ResetCardClickFlag()
        {
            canClick = true;
            cardCount = 0;
            lastClickedCardId = 0;
        }


        private void Congratulation()
        {
            canClick = false;
            cardView.Completed();
        }
    }
}