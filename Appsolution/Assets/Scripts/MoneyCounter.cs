using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
   public static int _allMoney;

    private int _moneyForGame;

    public static void SpendMoney(int money)
    {
        _allMoney -= money;
        PlayerPrefs.SetInt("AllMoney", _allMoney);
    }

    public static void ReceiveMoney(int money)
    {
        _allMoney += money;
        PlayerPrefs.SetInt("AllMoney", _allMoney);
    }

    public static void GetCurrentMoney()
    {
        if (PlayerPrefs.HasKey("AllMoney"))
        {
            _allMoney = PlayerPrefs.GetInt("AllMoney");
        }
    }
}
