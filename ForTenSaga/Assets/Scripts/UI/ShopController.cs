using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _coinsText;
    
    [SerializeField] private ShopElementComponent[] _shopElementComponent;

    public static Action updateShopPage;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("TigerShopFirstIsBuyed"))
        {
            _shopElementComponent[0].Buy();
            PlayerPrefs.SetInt("TigerShopFirstIsBuyed", 1);
        }
        updateShopPage += OnUpdateShopPage;
        OnUpdateShopPage();
    }

    private void LateUpdate()
    {
        _coinsText.text = "x" + GameManager.TigerCoinsCount.ToString();
    }

    private void OnDestroy()
    {
        updateShopPage -= OnUpdateShopPage;
    }
    
    private void OnUpdateShopPage()
    {
        foreach (var item in _shopElementComponent)
        {
            item.UpdateV();
        }
    }
}
