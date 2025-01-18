using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeprechauntCardComponent : MonoBehaviour, IPointerClickHandler
{
    private Image _myImage;

    private bool _isOnPressed;

    public static List<LeprechauntCardComponent> selectedsCardsPool = new List<LeprechauntCardComponent>();

    public Sprite myCardSprite;

    public Sprite cardBackSprite;

    public static bool canClick;

    public void Init(Sprite card)
    {
        _myImage = GetComponent<Image>();
        _myImage.sprite = cardBackSprite;
        myCardSprite = card;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canClick)
            return;
        if (_isOnPressed)
            return;
        if (!LeprecauntGamemanager._cardGameRunned)
            return;
        _isOnPressed = true;
        Open();
        selectedsCardsPool.Add(this);
        if (selectedsCardsPool.Count >= 2)
        {
            canClick = true;
            LeprecauntGamemanager.checkCards?.Invoke(selectedsCardsPool[0], selectedsCardsPool[1]);
        }
    }

    public void Open()
    {
        transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 180, 0), 0.25f).OnComplete(() => _myImage.sprite = myCardSprite);
    }

    public void OnDefault()
    {
        transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.25f).OnComplete(() => { _myImage.sprite = cardBackSprite; _isOnPressed = false; selectedsCardsPool.Clear(); });
    }
}
