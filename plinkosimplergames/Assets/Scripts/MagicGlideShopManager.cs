using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MagicGlideShopManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _magicGlideSkinIndex;
    [SerializeField] private Text _magicGlidePriceText;
    [SerializeField] private int _magicGlidePrice;
    [SerializeField] private MagicGlideShopManager[] _magicGlideShopManagers;

    private int _magicGlideShopIsBuyed
    {
        get
        {
            if (PlayerPrefs.HasKey("MagicGlideShopIsBuyedStateSaveKey" + _magicGlideSkinIndex))
            {
                return PlayerPrefs.GetInt("MagicGlideShopIsBuyedStateSaveKey" + _magicGlideSkinIndex);
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("MagicGlideShopIsBuyedStateSaveKey" + _magicGlideSkinIndex, value);
        }
    }

    private void Awake()
    {
        OnUpdateStatus();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_magicGlideShopIsBuyed != 0)
        {
            OnEquip();
        }
        else
        {
            OnBuy();
        }
    }

    public void OnBuy()
    {
        if (MagicGlideGameManager.MagicGlideStarsCount >= _magicGlidePrice)
        {
            MagicGlideGameManager.MagicGlideStarsCount -= _magicGlidePrice;
            _magicGlideShopIsBuyed = 1;
            OnEquip();
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    private void OnEquip()
    {
        MagicGlideGameManager.MagicGlideSkinIndex = _magicGlideSkinIndex;
        foreach (var item in _magicGlideShopManagers)
        {
            item.OnUpdateStatus();
        }
    }

    public void OnUpdateStatus()
    {
        if (_magicGlideShopIsBuyed != 0)
        {
            if (MagicGlideGameManager.MagicGlideSkinIndex != _magicGlideSkinIndex)
            {
                _magicGlidePriceText.text = "EQUIP";
            }
            else
            {
                _magicGlidePriceText.text = "EQUIPED";
            }
        }
        else
        {
            _magicGlidePriceText.text = "x" + _magicGlidePrice.ToString();
        }
    }
}
