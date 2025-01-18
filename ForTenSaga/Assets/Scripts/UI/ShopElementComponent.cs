using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopElementComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private int _price;

    [SerializeField] private int _index;

    private int _currentState
    {
        get => PlayerPrefs.GetInt($"TigerSkinBuyed{_index}",0);
        set => PlayerPrefs.SetInt($"TigerSkinBuyed{_index}", value);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_currentState != 0)
        {
            Equip();
        }
        else
        {
            Buy();
        }
    }

    public void Buy()
    {
        if (GameManager.TigerCoinsCount >= _price)
        {
            GameManager.TigerCoinsCount -= _price;
            _currentState = 1;
            Equip();
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    private void Equip()
    {
        GameManager.TigerSkinIndex = _index;
        ShopController.updateShopPage?.Invoke();
    }

    public void UpdateV()
    {
        if (_currentState != 0)
        {
            if (_index != GameManager.TigerSkinIndex)
            {
                _text.text = "EQUIP";
            }
            else
            {
                _text.text = "EQUIPPED";
            }
        }
        else
        {
            _text.text = "x" + _price.ToString();
        }
    }
}
