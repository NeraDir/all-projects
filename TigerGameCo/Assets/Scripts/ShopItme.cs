using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItme : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _lockPanel;
    [SerializeField] private TMP_Text _stateTxt;
    [SerializeField] private TMP_Text _priceTxt;
    [SerializeField] private Button _button;

    private ShopData _shopData;

    private bool isBought
    {
        get => bool.Parse(PlayerPrefs.GetString($"{_shopData.sprite}TigerClawsShopItemIsBoughtSaveKey","false"));
        set => PlayerPrefs.SetString($"{_shopData.sprite}TigerClawsShopItemIsBoughtSaveKey", value.ToString());
    }

    public void Init(ShopData data)
    {
        _shopData = data;
        MenuManager.onUpdateShop.AddListener(ShopItemUpdate);
        if (_shopData.index == 0)
        {
            OnBuy();
        }
    }

    private void Start()
    {
        ShopItemUpdate();
    }

    private void OnDestroy()
    {
        MenuManager.onUpdateShop.RemoveAllListeners();
    }

    private void ShopItemUpdate()
    {
        _image.sprite = _shopData.sprite;
        _stateTxt.text = isBought ? "BOUGHT" : "x" + _shopData.price.ToString();
        _priceTxt.text = isBought ? TigerClawsGameData.TigerClawsSelectedBackgroundIndex == _shopData.index ? "SELECTED" : "SELECT" : "BUY";
        _lockPanel.SetActive(!(_shopData.index <= TigerClawsGameData.TigerClawsMaxReachedLevels));
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            if (isBought) 
                OnEquip();
            else
                OnBuy();
        });
    }

    private void OnBuy()
    {
        if (TigerClawsGameData.TigerClawsUserCoins >= _shopData.price)
        {
            TigerClawsGameData.TigerClawsUserCoins -= _shopData.price;
            MenuManager.onUpdateCoins?.Invoke();
            isBought = true;
        }
    }

    private void OnEquip()
    {
        TigerClawsGameData.TigerClawsSelectedBackgroundIndex = _shopData.index;
        MenuManager.onUpdateShop?.Invoke();
    }

    public void BuyDestination()
    {
        isBought = true;
    }
}

[Serializable]
public struct ShopData
{
    public int price;
    public int index;
    public Sprite sprite;
}
