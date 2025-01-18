using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField]
    private Text _moneyForGameText;

    public static int _allMoney;

    public static int _moneyForGame;

    private void Start()
    {
        RedrawMoneyCount(_moneyForGameText, _moneyForGame);
    }

    public static void SpendMoney(int money)
    {
        _allMoney -= money;
        PlayerPrefs.SetInt("AllMoney", _allMoney);
    }

    public void ReceiveMoney(int money)
    {
        _allMoney += money;
        _moneyForGame += money;
        RedrawMoneyCount(_moneyForGameText, _moneyForGame);
        PlayerPrefs.SetInt("AllMoney", _allMoney);
    }

    public static void RedrawMoneyCount(Text moneyText, int moneyCount)
    {
       moneyText.text = $"x{moneyCount}";
    }

    public static void GetCurrentMoney()
    {
        if (PlayerPrefs.HasKey("AllMoney"))
        {
            _allMoney = PlayerPrefs.GetInt("AllMoney");
        }
    }
        
}
