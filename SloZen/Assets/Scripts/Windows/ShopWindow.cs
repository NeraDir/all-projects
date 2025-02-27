using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopWindow : Window
{
    [SerializeField] private ShopItemData[] _shopData;

    [SerializeField] private Text _coinsTxt;

    [SerializeField] private ShopItemComponent _itemPrefab;
    [SerializeField] private Transform _spawnPosition;

    public static int Coins
    {
        get => PlayerPrefs.GetInt("SloZenCurrentCoinsSaveKey", 100);
        set => PlayerPrefs.SetInt("SloZenCurrentCoinsSaveKey", value);
    }

    public static UnityEvent onUpdateShop = new UnityEvent();

    public override void Init()
    {
        foreach (var item in _shopData)
        {
            ShopItemComponent newItem = Instantiate(_itemPrefab, _spawnPosition);
            newItem.Init(item);
        }
        onUpdateShop.AddListener(OnUpdateCoins);
        onUpdateShop?.Invoke();
        base.Init();
    }

    private void OnDestroy()
    {
        onUpdateShop.RemoveListener(OnUpdateCoins);
    }

    private void OnUpdateCoins()
    {
        _coinsTxt.text = Coins.ToString("0");
    }
}

[Serializable]
public struct ShopItemData
{
    public Sprite sprite;
    public int price;
}
