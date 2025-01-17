using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _howtoPlayPage;

    [SerializeField]
    private GameObject _menuPage;

    [SerializeField]
    private Image _bgImage;

    [SerializeField]
    private Sprite[] _bgSprite;

    [Space(10)]
    [Header("Shop Data")]
    [SerializeField]
    private ShopContainer _shopContainer;

    [SerializeField]
    private Transform _shopSpawnPos;

    [SerializeField]
    private ShopData _shopData;

    [SerializeField]
    private TMP_Text _gPointsTxt;

    [Space(10)]
    [Header("Levels Data")]
    [SerializeField]
    private LevelContainer _levelContainerPrefab;

    [SerializeField]
    private Transform _levelsSpawnPos;

    [SerializeField]
    private LevelDatas _levelsSpawnData;


    private List<ShopContainer> _shopContainers = new List<ShopContainer>();

    public static Action onChangeStateOfShop;

    private void Awake()
    {
        onChangeStateOfShop += OnShopChanges;
        if (!PlayerPrefs.HasKey("CrazyHowToPlayShowSaveKey"))
        {
            _howtoPlayPage.SetActive(true);
            _menuPage.SetActive(false);
            PlayerPrefs.SetInt("CrazyHowToPlayShowSaveKey", 1);
        }

        foreach (var item in _shopData.shopDats)
        {
            ShopContainer tempShopContainer = Instantiate(_shopContainer, _shopSpawnPos);
            tempShopContainer.SetData(item.price, item.bgSprite,item.index);
            _shopContainers.Add(tempShopContainer);
        }

        for (int i = 0; i < _levelsSpawnData.levelPattern.Count; i++)
        {
            LevelContainer tempLevelContainer = Instantiate(_levelContainerPrefab, _levelsSpawnPos);
            tempLevelContainer.SetData(i);
        }
        _gPointsTxt.text = GameSavesData.PlayerGCoinsCount.ToString() + "G";
        _bgImage.sprite = _bgSprite[GameSavesData.SelectedBgIndex];
    }

    private void OnShopChanges()
    {
        foreach (var item in _shopContainers)
        {
            item.UpdateVisual();
        }
        _bgImage.sprite = _bgSprite[GameSavesData.SelectedBgIndex];
        _gPointsTxt.text = GameSavesData.PlayerGCoinsCount.ToString() + "G";
    }

    public void OnPlayButtonPressed()
    {
        StartCoroutine(Motion(false));
    }

    public void OnQuitButtonPressed()
    {
        StartCoroutine(Motion(true));
    }

    private IEnumerator Motion(bool value)
    {
        yield return new WaitForSeconds(0.5f);
        if (value)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("CrazyGameScene");
        }
    }
}
