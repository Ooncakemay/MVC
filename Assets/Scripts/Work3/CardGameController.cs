namespace Work3
{
    public class CardGameController : ICardGameController
    {
        private readonly ICardModel cardModel;
        private readonly ICardView cardView;

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
            if (CanFlip() is false)
            {
                return;
            }

            if (cardModel.IsFront(id))
            {
                return;
            }

            if (IsEmptyLastCard())
            {
                lastClickedCardId = id;
            }

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

            if (cardCount is MaxCardCount)
            {
                canClick = false;

                var match = cardModel.CheckMatch(id, lastClickedCardId);
                if (match)
                {
                    cardModel.SetCardMatch(id, lastClickedCardId);
                    ResetCardClickFlag();
                    if (cardModel.IsAllMatched())
                    {
                        Congratulation();
                    }
                }
                else
                {
                    FlipCardToBack(id, lastClickedCardId);
                }
            }
        }

        private bool CanFlip()
        {
            return canClick;
        }

        private bool IsEmptyLastCard()
        {
            return cardCount is 0;
        }

        private void FlipCardToFront(int id)
        {
            cardCount++;
            cardModel.FlipCard(id);
            cardView.ShowCardFront(id);
        }

        private void FlipCardToBack(params int[] ids)
        {
            foreach (var id in ids)
            {
                FlipCardToBack(id);
            }
        }

        private void FlipCardToBack(int id)
        {
            cardView.ShowCardBack(id);
            cardModel.FlipCard(id);
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