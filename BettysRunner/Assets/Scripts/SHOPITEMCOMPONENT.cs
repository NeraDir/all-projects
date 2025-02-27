using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SHOPITEMCOMPONENT : MonoBehaviour
{
    [SerializeField]
    private Text _priceTxt;

    [SerializeField]
    private Text _stateTxt;

    [SerializeField]
    private Image _image;

    private const string SHOP_ITEM_BOUGHT_SAVE_KEY = "SHOPITEMISBOUGHT";

    private ShopItem _itemData;

    private bool _isBought
    {
        get => bool.Parse(PlayerPrefs.GetString($"{SHOP_ITEM_BOUGHT_SAVE_KEY}{_itemData.id}", "false"));
        set => PlayerPrefs.SetString($"{SHOP_ITEM_BOUGHT_SAVE_KEY}{_itemData.id}", value.ToString());
    }

    public void Init(ShopItem item)
    {
        _itemData = item;
        if (_itemData.id == 0)
        {
            Buy();
        }
    }

    public void Buy()
    {
        if (_isBought)
        {
            PLAYERDATA.BACKGROUNDINDEX = _itemData.id;
            MENUMANAGER.ONUPDATEALLITEMS?.Invoke();
        }
        else
        {
            if (PLAYERDATA.COINS >= _itemData.price)
            {
                PLAYERDATA.COINS -= _itemData.price;
                _isBought = true;
                MENUMANAGER.ONUPDATEALLITEMS?.Invoke();
            }
            else
            {
                Handheld.Vibrate();
            }
        }
    }

    public void UpdateV()
    {
        _image.sprite = _itemData.sprite;
        if (_isBought) _priceTxt.gameObject.SetActive(false); 
        else _priceTxt.text = _itemData.price.ToString();
        _stateTxt.text = _isBought ? PLAYERDATA.BACKGROUNDINDEX == _itemData.id ? "EQUIPPED" : "EQUIP" : "BUY";
    }
}
