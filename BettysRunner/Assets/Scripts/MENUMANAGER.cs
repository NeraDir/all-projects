using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MENUMANAGER : MonoBehaviour
{
    [SerializeField]
    private Text[] _coinsTxt;

    [SerializeField] 
    private GameObject _aboutPage;

    [SerializeField]
    private GameObject _menuPage;

    [Header("SETTINGS")]
    [SerializeField]
    private Slider _musicSlider;

    [SerializeField]
    private Slider _soundSlider;

    [Header("SHOP")]
    [SerializeField]
    private SHOPDATA _shopData;

    [SerializeField]
    private Transform _shopContent;

    [SerializeField]
    private SHOPITEMCOMPONENT _shopItemComponent;

    [Header("RECORDS")]
    [SerializeField]
    private Transform _recordsContent;

    [SerializeField]
    private Text _recordsTxt;

    private List<SHOPITEMCOMPONENT> _listOfShopItems = new List<SHOPITEMCOMPONENT>();

    public static Action ONUPDATEALLITEMS;

    private void Awake()
    {
        UICUSTOMBUTTONCOMPONENT.buttonClicked = false;
        foreach (var item in PLAYERDATA.RECORDS)
        {
            Debug.Log(item);
        }
        if (!PLAYERDATA.FIRSTENTRY)
        {
            _aboutPage.SetActive(true);
            _menuPage.SetActive(false);
            PLAYERDATA.FIRSTENTRY = true;
        }

      
        _musicSlider.value = PLAYERDATA.MUSICVOLUME;
        _soundSlider.value = PLAYERDATA.SOUNDVOLUME;
        SetupShop();
        SetupRecords();
        ONUPDATEALLITEMS += OnUpdateItems;
    }

    private void OnDestroy()
    {
        ONUPDATEALLITEMS -= OnUpdateItems;
    }

    private void OnUpdateItems()
    {
        foreach (var item in _listOfShopItems)
        {
            item.UpdateV();
        }
    }

    private void SetupShop()
    {
        foreach (var item in _shopData.shopItems)
        {
            SHOPITEMCOMPONENT newItem = Instantiate(_shopItemComponent, _shopContent);
            newItem.Init(item);
            newItem.UpdateV();
            _listOfShopItems.Add(newItem);
        }
    }

    private void SetupRecords()
    {
        for (int i = 0; i < PLAYERDATA.RECORDS.Count; i++)
        {
            Text newItem = Instantiate(_recordsTxt, _recordsContent);
            newItem.text = (i + 1).ToString() + " - " + PLAYERDATA.RECORDS[i];
        }
    }

    private void LateUpdate()
    {
        foreach (var item in _coinsTxt)
        {
            item.text = PLAYERDATA.COINS.ToString();
        }

        PLAYERDATA.MUSICVOLUME = _musicSlider.value;
        PLAYERDATA.SOUNDVOLUME = _soundSlider.value;
    }

    public void OpenGame()
    {
        SceneManager.LoadScene("Game");
    }
}
