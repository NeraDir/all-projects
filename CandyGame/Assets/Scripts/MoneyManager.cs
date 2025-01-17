using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static int money;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Money") == 0)
        {
            SetMoney(30);
        }
      
    }

    private void Update()
    {
        moneyText.text = PlayerPrefs.GetInt("Money").ToString();
        money = PlayerPrefs.GetInt("Money");

    }

    public static void SetMoney(int amount)
    {
       money = PlayerPrefs.GetInt("Money", 0);
       money += amount;
       PlayerPrefs.SetInt("Money", money);
    }

    [ContextMenu("GetMoney")]
    public void GetMoney()
    {
        SetMoney(700);
    }
}
