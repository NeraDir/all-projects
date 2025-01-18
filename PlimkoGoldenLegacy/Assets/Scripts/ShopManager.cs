using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private ShopManager[] _otherShopContainers;

    [SerializeField]
    private TMP_Text _priceTXT;

    [SerializeField]
    private int _indexOfManager;

    [SerializeField]
    private int _shopManagerPrice;

    [SerializeField]
    private string _shopManagerSavingAddString;

    [SerializeField]
    private Button _shopmanagerButton;

    [SerializeField]
    private TMP_Text _shopManagerCoinsZero;

    private int _shopManagerBuyedState 
    {
        get 
        {
            if (PlayerPrefs.HasKey($"_shopManagerBuyedState{_indexOfManager}{_shopManagerSavingAddString}"))
            {
                return PlayerPrefs.GetInt($"_shopManagerBuyedState{_indexOfManager}{_shopManagerSavingAddString}");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt($"_shopManagerBuyedState{_indexOfManager}{_shopManagerSavingAddString}", value);
        }
    }

    [SerializeField]
    private bool _isPlatform;

    private void Start()
    {
        _shopmanagerButton.onClick.AddListener(OnShopManagerBuyPressed);
        UpdateShopState();
    }

    public void UpdateShopState() 
    {
        if (_shopManagerBuyedState != 0)
        {
            if (_isPlatform)
            {
                if (GameManager.selectedPlatform == _indexOfManager)
                {
                    _priceTXT.text = "EQUIPPED";
                }
                else
                {
                    _priceTXT.text = "";
                }
            }
            else
            {
                if (GameManager.selectedBall == _indexOfManager)
                {
                    _priceTXT.text = "+";
                }
                else
                {
                    _priceTXT.text = "";
                }
            }
        }
        else
        {
            if (_isPlatform)
            {
                _priceTXT.text = _shopManagerPrice.ToString();
            }
            else
            {
                _priceTXT.text = _shopManagerPrice.ToString();
            }
        }
    }


    public void OnShopManagerBuyPressed() 
    {
        if (_shopManagerBuyedState != 0)
        {
            OnEquipPressed();
            UpdateShopState();
            foreach (var item in _otherShopContainers)
            {
                item.UpdateShopState();
            }
        }
        else
        {
            if (GameManager.coins >= _shopManagerPrice)
            {
                GameManager.coins -= _shopManagerPrice;
                _shopManagerBuyedState = 1;
                OnEquipPressed();
                UpdateShopState();
                foreach (var item in _otherShopContainers)
                {
                    item.UpdateShopState();
                }
            }
            else
            {
                Instantiate(_shopManagerCoinsZero, transform.parent);
            }
        }
    }

    private void OnEquipPressed() 
    {
        if (_isPlatform) 
        {
            GameManager.selectedPlatform = _indexOfManager;
        }
        else
        {
            GameManager.selectedBall = _indexOfManager;
        }
    }
}
