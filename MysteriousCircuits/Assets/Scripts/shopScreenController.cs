using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopScreenController : MonoBehaviour
{
    [SerializeField]
    private List<ShopData> _shopDatas = new List<ShopData>();

    [SerializeField]
    private shopComponent _shopComponentPrefab;

    [SerializeField]
    private Transform _shopParent;

    private List<shopComponent> _shopComponents = new List<shopComponent>();

    public static Action updateShopScreen;

    public void Awake()
    {
        PlayerPrefs.SetInt($"MysteriousCircuitsShopComponentIsBuyedState{_shopDatas[0].name}", 1);
        foreach (var item in _shopDatas)
        {
            shopComponent newShop = Instantiate(_shopComponentPrefab, _shopParent);
            newShop.Init(item);
            _shopComponents.Add(newShop);
        }
        updateShopScreen += OnShopUpdate;
        menuScreenController.updateDiamondCount?.Invoke();
    }

    private void OnDestroy()
    {
        updateShopScreen -= OnShopUpdate;
    }

    private void OnShopUpdate()
    {
        foreach (var item in _shopComponents)
        {
            item.VisualUpdate();
        }
    }
}

[Serializable]
public struct ShopData
{
    public int price;
    public string name;
    public Sprite sprite;
    public int index;
}
