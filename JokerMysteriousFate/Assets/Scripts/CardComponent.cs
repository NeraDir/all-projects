using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour, IPointerClickHandler
{
    private bool isClicked;

    public static List<CardComponent> selectedCards = new List<CardComponent>();

    public Sprite cardSprite;

    public Sprite cardDefaultSprite;

    private Image cardImage;

    public static bool canClick;

    public void Init(Sprite card) 
    {
        cardImage = GetComponent<Image>();
        cardImage.sprite = cardDefaultSprite;
        cardSprite = card;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canClick)
            return;
        if (isClicked)
            return;
        if (!TestControllersComponent.isGameStarted)
            return;
        isClicked = true;
        Open();
        selectedCards.Add(this);
        if (selectedCards.Count >= 2)
        {
            canClick = true;
            TestControllersComponent.checkCards?.Invoke(selectedCards[0], selectedCards[1]);
        }
    }

    public void Open() 
    {
        transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 180, 0), 0.25f).OnComplete(() => cardImage.sprite = cardSprite);
    }

    public void OnDefault() 
    {
        transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.25f).OnComplete(() => { cardImage.sprite = cardDefaultSprite; isClicked = false; selectedCards.Clear(); });
    }
}