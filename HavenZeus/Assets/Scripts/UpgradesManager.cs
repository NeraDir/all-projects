using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField]
    private int _healthUpgradeCost;
    [SerializeField]
    private int _damageUpgradeCost;
    [SerializeField]
    private float _healthUpgradeValue;
    [SerializeField]
    private float _damageUpgradeValue;
    [SerializeField]
    private Text _healthUpgradeCostText;
    [SerializeField]
    private Text _damageUpgradeCostText;
    [SerializeField]
    private Text _healthUpgradeLevelText;
    [SerializeField]
    private Text _damageUpgradeLevelText;
    [SerializeField]
    private Text _allMoney;
    [SerializeField]
    private Button _healthUpgradeButton;
    [SerializeField]
    private Button _damageUpgradeButton;

    private int _healthUpgradeLevel;
    private int _damageUpgradeLevel;

    public static float _bulletDamage = 10f;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HealthLevelUpgrade"))
        {
            _healthUpgradeLevel = PlayerPrefs.GetInt("HealthLevelUpgrade");
        }

        if (PlayerPrefs.HasKey("DamageLevelUpgrade"))
        {
            _damageUpgradeLevel = PlayerPrefs.GetInt("DamageLevelUpgrade");
        }

        _healthUpgradeCostText.text = _healthUpgradeCost.ToString();
        _damageUpgradeCostText.text = _damageUpgradeCost.ToString();
        _healthUpgradeLevelText.text = _healthUpgradeLevel.ToString();

        RedrawMoney();
        
    }

    private void Update()
    {
        if(MoneyCounter._allMoney < _healthUpgradeCost)
        {
            _healthUpgradeButton.interactable = false;
        }

        if (MoneyCounter._allMoney < _damageUpgradeCost)
        {
            _damageUpgradeButton.interactable = false;
        }
    }
    public void HealthUpgrade()
    {
        MoneyCounter.SpendMoney(_healthUpgradeCost);
        RedrawMoney();
        HeroHealthSystem._maxHealth += _healthUpgradeValue;
        _healthUpgradeLevel++;
        _healthUpgradeLevelText.text = $"LVL {_healthUpgradeLevel}";
        PlayerPrefs.SetFloat("MaxHealth", HeroHealthSystem._maxHealth);
        PlayerPrefs.SetInt("HealthLevelUpgrade", _healthUpgradeLevel);
    }

    public void DamageUpgrade()
    {
        MoneyCounter.SpendMoney(_damageUpgradeCost);
        RedrawMoney();
        _bulletDamage += _damageUpgradeValue;
        _damageUpgradeLevel++;
        _damageUpgradeLevelText.text = $"LVL {_damageUpgradeLevel}";
        PlayerPrefs.SetFloat("MaxDamage", _bulletDamage);
        PlayerPrefs.SetInt("DamageLevelUpgrade", _damageUpgradeLevel);
     
    }

    public void RedrawMoney()
    {
        MoneyCounter.GetCurrentMoney();
        _allMoney.text = $"x{MoneyCounter._allMoney}";
    }
}
