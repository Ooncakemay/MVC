using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int index;
    public bool isJoker;
    public Sprite content;
    public Sprite back;

    [SerializeField] private Image image;
    [SerializeField] private Button button;
    public bool isFlipped { get; private set; }

    public void ShowCardBack()
    {
        image.sprite = back;
        isFlipped = false;
    }

    public void ShowCardContent()
    {
        image.sprite = content;
        isFlipped = true;
    }

    public void InitButton(Action<int> flipCard)
    {
        button.onClick.AddListener(delegate { flipCard(index); });
    }
}