using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopContainer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _priceTxt;

    [SerializeField]
    private TMP_Text _stateOfContainer;

    [SerializeField]
    private Image _bgIamge;

    private Sprite _bgSprite;
    private int _price;
    private int _index;

    private int isBuy
    {
        get
        {
            if (PlayerPrefs.HasKey($"CrazyShopIsBuySaveKey{_price}"))
            {
                return PlayerPrefs.GetInt($"CrazyShopIsBuySaveKey{_price}");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt($"CrazyShopIsBuySaveKey{_price}", value);
        }
    }

    private void Start()
    {
        UpdateVisual();
    }

    public void SetData(int price,Sprite bg,int index)
    {
        _bgSprite = bg; 
        _price = price;
        _index = index;

    }

    public void OnClickButton()
    {
        if (isBuy != 0)
        {
            Equip();
        }
        else
        {
            if (GameSavesData.PlayerGCoinsCount >= _price)
            {
                GameSavesData.PlayerGCoinsCount -= _price;
                isBuy = 1;
                Equip();
            }
            else
            {
                Handheld.Vibrate();
            }
        }
    }

    private void Equip()
    {
        GameSavesData.SelectedBgIndex = _index;
        MenuManager.onChangeStateOfShop?.Invoke();
    }

    public void UpdateVisual()
    {
        _bgIamge.sprite = _bgSprite;
        if (isBuy != 0)
        {
            _priceTxt.text = "BOUGHT";
            if (GameSavesData.SelectedBgIndex == _index)
            {
                _stateOfContainer.text = "EQUIPPED";
            }
            else
            {
                _stateOfContainer.text = "EQUIP";
            }
        }
        else
        {
            _priceTxt.text = _price.ToString() + "G";
        }
    }
}
