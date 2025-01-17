using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeItem : MonoBehaviour
{
    [SerializeField]
    private UpgradeTypes upgradeItem;

    [SerializeField]
    private TMP_Text levelNumberText;
    [SerializeField]
    private TMP_Text priceText;

    private int levelNumber;
    [SerializeField]
    private int price;

    
    private void OnEnable()
    {
        levelNumber = GetLevelNumber();
        SetPrice();
    }

    private void Update()
    {
        levelNumberText.text = "LEVEL " + levelNumber;
    }

    public int GetLevelNumber()
    {
        if (upgradeItem == UpgradeTypes.Health)
        {
            return GamePlayConfigs.healthLevelNumber;
        }
        else if(upgradeItem == UpgradeTypes.Energy)
        {
            return GamePlayConfigs.energyLevelNumber;
        }
        else
        {
            return GamePlayConfigs.damageLevelNumber;
        }

    }

    public void SetPrice()
    {
        price = 100 + ((levelNumber - 1) * 10);
    }

    public void TapUpgradeButton()
    {
        if (UI_UpgradePage.coinCount - price >= 0)
        {
            UI_UpgradePage.IncrementCoins(-price);
            levelNumber++;
            SaveLevelNumber();
        }
    }

    public void SaveLevelNumber()
    {
        if (upgradeItem == UpgradeTypes.Health)
        {
            GamePlayConfigs.healthLevelNumber = levelNumber; 
        }
        else if (upgradeItem == UpgradeTypes.Energy)
        {
            GamePlayConfigs.energyLevelNumber = levelNumber;
        }
        else
        {
            GamePlayConfigs.damageLevelNumber = levelNumber;
        }
    }
}


public enum UpgradeTypes
{
    Health,
    Energy,
    Damage
}