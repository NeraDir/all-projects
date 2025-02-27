using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TlineMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _howToPlayScreen;
    [SerializeField] private GameObject _menuScreen;

    [Space(15)]
    [SerializeField] private LevelContainer _containerPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private int _count = 10;

    [Space(15)]
    [SerializeField] private ShopContainer _sContainerPrefab;
    [SerializeField] private Transform _sContent;
    [SerializeField] private ShopDatas _sShopDatas;

    [Space(15)]
    [SerializeField] private Text _coinsTxt;

    [Space(15)]
    [SerializeField] private GameObject _dailyScreen;
    [SerializeField] private Animator _anim;

    private List<ShopContainer> _currentShopContainers = new List<ShopContainer>();

    public static Action sendUpdateShop;

    private void Awake()
    {
        SetupLevelScreen();
        SetupShopScreen();

        if (!PlayerPrefs.HasKey("TlineHowToPlayDisplayedSaveKey"))
        {
            _howToPlayScreen.SetActive(true);
            _menuScreen.SetActive(false);
            _currentShopContainers[0].Buy();
            PlayerPrefs.SetInt("TlineHowToPlayDisplayedSaveKey", 1);
        }
        else
        {
            StartCoroutine(Checking());
        }

        sendUpdateShop += OnUpdateShop;
        OnUpdateShop();
    }

    private void OnDestroy()
    {
        sendUpdateShop -= OnUpdateShop;
    }

    private void SetupLevelScreen()
    {
        for (int i = 0; i < _count; i++)
        {
            LevelContainer newContainer = Instantiate(_containerPrefab, _content);
            newContainer.SetupData(i);
        }
    }

    private void SetupShopScreen()
    {
        for (int i = 0; i < _sShopDatas.shopDatas.Count; i++)
        {
            ShopContainer newContainer = Instantiate(_sContainerPrefab, _sContent);
            ShopData shopData = _sShopDatas.shopDatas[i];
            newContainer.SetupData(shopData.index, shopData.price, shopData.sprite);
            _currentShopContainers.Add(newContainer);
        }
    }

    private void OnUpdateShop()
    {
        foreach (var container in _currentShopContainers)
        {
            container.UpdateVisual();
        }
        _coinsTxt.text = TlineGameDataSaves.TlineCoins.ToString();
    }

    private IEnumerator Checking()
    {
        while (true)
        {
            CheckDaily();
            yield return new WaitForSeconds(1);
        }
    }

    private void CheckDaily()
    {
        if (TlineGameDataSaves.TlineLastClaimedDailyBonus != null)
        {
            TimeSpan? timeSpan = TlineGameDataSaves.TlineLastClaimedDailyBonus - DateTime.UtcNow;
            if (timeSpan.Value.TotalMilliseconds <= 0 && timeSpan.Value.TotalSeconds <= 0 && timeSpan.Value.TotalMinutes <= 0 && timeSpan.Value.TotalHours <= 0 && timeSpan.Value.TotalDays <= 0)
            {
                _dailyScreen.SetActive(true);
                _menuScreen.SetActive(false);
            }
        }
        else
        {
            _dailyScreen.SetActive(true);
            _menuScreen.SetActive(false);
        }
    }

    public void OnClickSpin()
    {
        _anim.SetBool("Spining", true);
        TlineGameDataSaves.TlineLastClaimedDailyBonus = DateTime.UtcNow.AddDays(1);
    }

    public void OnClickCloseHowToPlay()
    {
        if (TlineGameDataSaves.TlineLastClaimedDailyBonus != null)
        {
            TimeSpan? timeSpan = TlineGameDataSaves.TlineLastClaimedDailyBonus - DateTime.UtcNow;
            if (timeSpan.Value.TotalMilliseconds <= 0 && timeSpan.Value.TotalSeconds <= 0 && timeSpan.Value.TotalMinutes <= 0 && timeSpan.Value.TotalHours <= 0 && timeSpan.Value.TotalDays <= 0)
            {
                _dailyScreen.SetActive(true);
                _menuScreen.SetActive(false);
            }
            else
            {
                _howToPlayScreen.SetActive(false);
                _menuScreen.SetActive(true);
            }
        }
        else
        {
            _howToPlayScreen.SetActive(false);
            _dailyScreen.SetActive(true);
            _menuScreen.SetActive(false);
        }
    }
}
