using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Text _priceShow;

    [SerializeField]
    private int _shopId;

    [SerializeField]
    private int _shopPrice;

    private int _shopIsBuyed
    {
        get
        {
            if (PlayerPrefs.HasKey("ShopBeastPowerSaveKey" + _shopId))
            {
                return PlayerPrefs.GetInt("ShopBeastPowerSaveKey" + _shopId);
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ShopBeastPowerSaveKey" + _shopId, value);
        }
    }

    private void Start()
    {
        UpdateStatus();
        GetComponent<Button>().onClick.AddListener(OnClickBuy);
    }

    public void OnClickBuy()
    {
        if (_shopIsBuyed == 0)
        {
            if (GameManager.Coins >= _shopPrice)
            {
                GameManager.Coins -= _shopPrice;
                _shopIsBuyed = 1;
                OnClickEquip();
            }
            else
            {
                Handheld.Vibrate();
            }
        }
        else
        {
            OnClickEquip();
        }
    }

    public void OnClickEquip() 
    {
        GameManager.PantherSkinIndex = _shopId;
        FindObjectOfType<MenuManager>().UpdateAllShops();
    }

    public void UpdateStatus()
    {
        if (_shopIsBuyed == 0)
        {
            _priceShow.text = _shopPrice.ToString();
        }
        else
        {
            if (GameManager.PantherSkinIndex == _shopId)
            {
                _priceShow.text = "EQUIPED";
            }
            else
            {
                _priceShow.text = "EQUIP";
            }
        }
    }
}
