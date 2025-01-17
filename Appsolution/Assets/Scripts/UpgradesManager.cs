using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField]
    private int _upgradeCost;
    [SerializeField]
    private int _upgradeValue;
    [SerializeField]
    private Button _upgradeButton;
    [SerializeField]
    private Text _allMoneyText;

    private void Start()
    {
        MoneyCounter.GetCurrentMoney();
    }
    public void BuyUpgrade()
    {
        MoneyCounter.SpendMoney(_upgradeCost);
        CarMovement.UpgradeSpeed(_upgradeValue);
    }

    public static int delliveryCount
    {
        get
        {
            if (PlayerPrefs.HasKey("delliveryCountSaveKey"))
            {
                return PlayerPrefs.GetInt("delliveryCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("delliveryCountSaveKey", value);
        }
    }

    public static int delliveryCarSpeedValue
    {
        get
        {
            if (PlayerPrefs.HasKey("delliveryCarSpeedValueSaveKey"))
            {
                return PlayerPrefs.GetInt("delliveryCarSpeedValueSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("delliveryCarSpeedValueSaveKey", value);
        }
    }

    public void Update()
    {
        _allMoneyText.text = $"{MoneyCounter._allMoney}x";

        if (MoneyCounter._allMoney < _upgradeCost)
        {
            _upgradeButton.interactable = false;
        }
    }
}
