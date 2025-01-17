using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseMenuComponent : MonoBehaviour
{
    [SerializeField] private GameObject _chaseGameInfoPage;
    
    [SerializeField] private ChaseLevelCiontainerComponent _chaseLevelCiontainer;
    [SerializeField] private Transform _chaseLevelsParent;

    [SerializeField] private ChaseShopComponent _chaseShopPrefab;
    [SerializeField] private Transform _chaseShopsParent;
    
    [SerializeField] private ChaseDataOfShop _chaseDataOfShop;
    private List<ChaseShopComponent> _currentShopsComponents = new List<ChaseShopComponent>();
    
    [SerializeField] private Image[] _backgroundImages;

    [SerializeField] private Text[] _balanceText;
    
    public static Action onChaseShopAction;
    
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("ChaseGameInfoPageDisplayedKey"))
        {
            _chaseGameInfoPage.SetActive(true);
            PlayerPrefs.SetInt("ChaseGameInfoPageDisplayedKey", 1);
        }
        SetupChaseLevels(90);
        SetupChaseShop();
        onChaseShopAction += UpdateShop;
    }

    private void OnDestroy()
    {
        onChaseShopAction -= UpdateShop;
    }
    
    private void SetupChaseLevels(int levelsCount)
    {
        for (int i = 0; i < levelsCount; i++)
        {
            ChaseLevelCiontainerComponent newContainer = Instantiate(_chaseLevelCiontainer, _chaseLevelsParent);
            newContainer.level = i;
        }
    }

    private void Update()
    {
        foreach (var component in _balanceText)
        {
            component.text = ChasePlayerDataComponent.ChasePlayerCoins.ToString();
        }
    }

    private void UpdateShop()
    {
        foreach (var item in _currentShopsComponents)
        {
            item.UpdateVisual();
        }

        foreach (var item in _backgroundImages)
        {
            item.sprite = Resources.Load<Sprite>(ChasePlayerDataComponent.ChasePlayerBackgroundSpriteName);
        }
    }
    
    private void SetupChaseShop()
    {
        foreach (var item in _chaseDataOfShop.shopData)
        {
            ChaseShopComponent newShopComponent = Instantiate(_chaseShopPrefab, _chaseShopsParent);
            newShopComponent.SetData(item.sprite, item.price);
            _currentShopsComponents.Add(newShopComponent);
        }
        _currentShopsComponents[0].Buy();
        UpdateShop();
    }
}
