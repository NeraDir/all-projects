using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField]
    private Text _moneyInGameText;

    public static int _currentMoney;

    public static int _moneyForGame;

    private void Awake()
    {
        _currentMoney = 0;
        _moneyForGame = 0;
        GetCurrentGold();
    }
    public static void AddMoney(int addMoney)
    {
        _currentMoney += addMoney;
        _moneyForGame += addMoney;

        PlayerPrefs.SetInt("CurrentMoney", _currentMoney);
    }

    public static void SpendMoney(int spendMoney)
    {
        _currentMoney -= spendMoney;
        PlayerPrefs.SetInt("CurrentMoney", _currentMoney);
    }

    public static void GetCurrentGold()
    {
        if (PlayerPrefs.HasKey("CurrentMoney"))
        {
            _currentMoney = PlayerPrefs.GetInt("CurrentMoney");
        }
    }

    public void RedarawGameMoney()
    {
        _moneyInGameText.text = $"x{_moneyForGame.ToString()}";
    }
}
