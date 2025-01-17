using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Text _allMoneyText;
    [SerializeField]
    private ShopItem[] _shopItems;

    public static int _currentSkinIndex;

    private void Start()
    {
        if (PlayerPrefs.HasKey("CurrentSkinIndex"))
        {
            _currentSkinIndex = PlayerPrefs.GetInt("CurrentSkinIndex");
        }

        CheckCurrentSkin();

        MoneyCounter.GetCurrentMoney();
    }

    private void Update()
    {
        _allMoneyText.text = $"{MoneyCounter._allMoney}x";
    }

    public void SelectCurrentSkin(int currentSkinIndex)
    {
        _currentSkinIndex = currentSkinIndex;
        PlayerPrefs.SetInt("CurrentSkinIndex", _currentSkinIndex);

        CheckCurrentSkin();
    }

    public void CheckCurrentSkin()
    {
        foreach(ShopItem item in _shopItems)
        {
            item.RedrawBuyButton(_currentSkinIndex);
        }
    }
}
