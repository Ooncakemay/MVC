using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//此為一個對對碰遊戲
//遊戲開始時從圖庫中隨機選出不重複的四種卡面和一張鬼牌，亂序套到台面九張牌上
//翻到正確兩張牌則配對成功，翻到錯誤則兩張牌同時翻回背面，翻到鬼牌則場上所有牌都翻回背面
//除鬼牌以外全部配對正確則過關

public class CardMatchingGame : MonoBehaviour 
{
    [SerializeField] private Sprite[] backgroundPictures;
    private const int backgroundTypeCount = 2;
    [SerializeField] private Image background;

    [SerializeField] private Sprite cardBack;
    [SerializeField] private Sprite cardJoker;
    [SerializeField] private List<Sprite> cardTypes;

    [SerializeField] private List<Card> cards;
    private const int cardTypeCount = 6;

    [SerializeField] private GameObject mask;
    [SerializeField] private Text systemMsg;

    private List<Sprite> randomPickedContents;
    private bool hasPickedPreviousCard;
    private int previousPickedCardIndex;
    private bool isDisplaying;
    private Color transparent = new Color(1, 1, 1, 0);

    private readonly int targetScore = 4;
    private int score;

    void Start () 
    {
        Initialization();
	}
	
    private void Initialization()
    {
        InitCards();
        AssignRandomBackground();
        AssignCards();

        hasPickedPreviousCard = false;
        systemMsg.color = transparent;
        mask.SetActive(false);
    }

    private void InitCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].index = i;
            cards[i].back = cardBack;
            cards[i].InitButton(FlipCard);
            cards[i].ShowCardBack();
        }
    }

    private void AssignRandomBackground()
    {
        int index = UnityEngine.Random.Range(0, backgroundTypeCount);
        background.sprite = backgroundPictures[index];
    }

    private void AssignCards()
    {
        PickFourRandomCardContents();
        AssignCardContent(GetUnorderedNumbers());
    }

    private void PickFourRandomCardContents()
    {
        randomPickedContents = new List<Sprite>();
        var unrepeatedNums = new List<int>();
        int cardPairs = 0;
        while (cardPairs < targetScore)
        {
            int randomCardType = UnityEngine.Random.Range(0, cardTypeCount);
            if (!unrepeatedNums.Contains(randomCardType))
            {
                cardPairs++;
                unrepeatedNums.Add(randomCardType);
                randomPickedContents.Add(cardTypes[randomCardType]);
            }
        }
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

    private void AssignCardContent(List<int> unorderedNums)
    {
        var contents = GetCardContents();
        Card tempCard;

        for (int i = 0; i < contents.Length; i++)
        {
            tempCard = cards[unorderedNums[i]];
            tempCard.content = contents[i];
            tempCard.isJoker = (i == 0);
        }
    }

    private Sprite[] GetCardContents()
    {
        Sprite[] contents = new Sprite[9];

        contents[0] = cardJoker;
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

    private void FlipCard(int index)
    {
        if (!cards[index].isFlipped && !isDisplaying)
        {
            cards[index].ShowCardContent();

            if (cards[index].isJoker)
            {
                hasPickedPreviousCard = false;
                StartCoroutine(FlipAllBack());
            }
            else
            {
                if (hasPickedPreviousCard)
                {
                    hasPickedPreviousCard = false;
                    CheckCardContent(previousPickedCardIndex, index);
                }
                else
                {
                    previousPickedCardIndex = index;
                    hasPickedPreviousCard = true;
                }
            }
        }
    }

    private IEnumerator FlipAllBack()
    {
        isDisplaying = true;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].ShowCardBack();
        }
        isDisplaying = false;
        score = 0;
    }

    private void CheckCardContent(int pre, int now)
    {
        if(cards[pre].content != cards[now].content)
        {
            StartCoroutine(FlipBack(pre, now));
        }
        else
        {
            score++;
            if (IsCleared())
            {
                StartCoroutine(ShowGameCompleted());
            }
        }
    }

    private IEnumerator FlipBack(int pre, int now)
    {
        isDisplaying = true;
        yield return new WaitForSeconds(1f);

        cards[pre].ShowCardBack();
        cards[now].ShowCardBack();

        isDisplaying = false;
    }

    private bool IsCleared()
    {
        return score == targetScore;
    }

    private IEnumerator ShowGameCompleted()
    {
        mask.SetActive(true);
        systemMsg.text = "Congratulations!";
        float alpha = 0.05f;
        while(alpha < 1)
        {
            alpha += 0.05f;
            systemMsg.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(3f);
        Debug.Log("遊戲結束");
    }
}