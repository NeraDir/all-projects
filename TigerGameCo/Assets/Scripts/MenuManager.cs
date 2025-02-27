using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _hwpScreen;
    [SerializeField] private TMP_Text[] _coinsTxt;

    [Header("Shop")]
    [SerializeField] private ShopData[] _shopData;
    [SerializeField] private ShopItme _itemPrefab;
    [SerializeField] private Transform _itemContent;

    [Header("Levels")]
    [SerializeField] private Transform[] _levelsContent;

    public static UnityEvent onUpdateShop = new UnityEvent();
    public static UnityEvent onUpdateCoins = new UnityEvent();

    private void Awake()
    {
        SetupShop();
        SetupLevels();
        OnUpdateCoins();
        onUpdateCoins.AddListener(OnUpdateCoins);
    }

    private void OnDestroy()
    {
        onUpdateCoins.RemoveAllListeners();
    }

    private void SetupShop()
    {
        foreach (var item in _shopData)
        {
            ShopItme newShopItem = Instantiate(_itemPrefab, _itemContent);
            newShopItem.Init(item);
        }
        onUpdateShop?.Invoke();
    }

    private void SetupLevels()
    {
        int index = 0;
        List<LevelItem> items = new List<LevelItem>();
        foreach (var item in _levelsContent)
        {
            for (int i = 0; i < item.GetComponentsInChildren<LevelItem>().Length; i++)
            {
                index += 1;
                items.Add(item.GetComponentsInChildren<LevelItem>()[i]);
            }
        }
        for (int i = 0; i < index; i++)
        {
            items[i].Init(i);
        }
    }

    private void OnUpdateCoins()
    {
        foreach (var item in _coinsTxt)
            item.text = "x" + TigerClawsGameData.TigerClawsUserCoins.ToString();
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }
}
