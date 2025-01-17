using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField]
    private int _healthUpgradeCost;
    [SerializeField]
    private int _fuelUpgradeCost;
    [SerializeField]
    private int _armourUpgradeCost;
    [SerializeField]
    private int _healthUpgradeValue;
    [SerializeField]
    private int _fuelUpgradeValue;
    [SerializeField]
    private int _armourUpgradeValue;
    [SerializeField]
    private Text _healthUpgradeCostText;
    [SerializeField]
    private Text _fuelUpgradeCostText;
    [SerializeField]
    private Text _armourUpgradeCostText;
    [SerializeField]
    private Text _healthUpgradeValueText;
    [SerializeField]
    private Text _fuelUpgradeValueText;
    [SerializeField]
    private Text _armourUpgradeValueText;

    [SerializeField]
    private Button _healthUpgradeButton;
    [SerializeField]
    private Button _fuelUpgradeButton;
    [SerializeField]
    private Button _armourUpgradeButton;

    private void Awake()
    {
        _healthUpgradeCostText.text = _healthUpgradeCost.ToString();
        _fuelUpgradeCostText.text = _fuelUpgradeCost.ToString();
        _armourUpgradeCostText.text = _armourUpgradeCost.ToString();

        _healthUpgradeValueText.text = _healthUpgradeValue.ToString();
        _fuelUpgradeValueText.text = _fuelUpgradeValue.ToString();
        _armourUpgradeValueText.text = _armourUpgradeValue.ToString();

        MoneyCounter.GetCurrentGold();
    }

    private void Update()
    {
        if(MoneyCounter._currentMoney < _healthUpgradeCost)
        {
            _healthUpgradeButton.interactable = false;
        }
        if (MoneyCounter._currentMoney < _fuelUpgradeCost)
        {
            _fuelUpgradeButton.interactable = false;
        }
        if (MoneyCounter._currentMoney < _armourUpgradeCost)
        {
            _armourUpgradeButton.interactable = false;
        }
    }

    public void HealthUpgrade()
    {
        if(MoneyCounter._currentMoney >= _healthUpgradeCost) 
        {
            HeroHealthSystem._maxHealth += _healthUpgradeValue;
            MoneyCounter.SpendMoney(_healthUpgradeCost);
            PlayerPrefs.SetFloat("MaxHealth", HeroHealthSystem._maxHealth);
        }
    }

    public void FuelUpgrade()
    {
        if (MoneyCounter._currentMoney >= _fuelUpgradeCost)
        {
            PlaneMovement._maxFuelCount += _fuelUpgradeValue;
            MoneyCounter.SpendMoney(_fuelUpgradeCost);
            PlayerPrefs.SetFloat("MaxFuel", PlaneMovement._maxFuelCount);
        }
    }

    public void ArmourUpgrade()
    {
        if (MoneyCounter._currentMoney >= _armourUpgradeCost)
        {
            HeroHealthSystem._maxArmour += _armourUpgradeValue;

            if(HeroHealthSystem._maxArmour > 80)
            {
                HeroHealthSystem._maxArmour = 80;
            }

            MoneyCounter.SpendMoney(_armourUpgradeCost);
            PlayerPrefs.SetFloat("MaxArmour", HeroHealthSystem._maxArmour);
        }
    }

}
