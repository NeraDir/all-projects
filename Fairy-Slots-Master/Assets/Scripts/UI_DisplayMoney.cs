using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UI_DisplayMoney : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyTMP;

    private float moneyLerp;

    public static int money;

    private static BoatGameData boatGameData;

    [SerializeField]
    private GameObject _recoverButtton;

    private void OnEnable()
    {
        money = BoatGameData.allCoinsCount;

        if (money < (100 + ((BoatGameData.betValueLevelNumber - 1) * 15)))
        {
            if (_recoverButtton != null)
            {
                _recoverButtton.SetActive(true);
            }
            
        }

    }

    private void Update()
    {
        moneyLerp = Mathf.Lerp(moneyLerp, money, 0.2f);

        if (moneyLerp == 0)
        {
            moneyTMP.text = "0";
        }
        else
        {
            moneyTMP.text = moneyLerp.ToString("#");
        }
        
    }

    public static void UpdateMoney(int value)
    {
        money += value;
        BoatGameData.allCoinsCount = money;

    }

    public void TapRecoverBtn()
    {
        money = 100 + ((BoatGameData.betValueLevelNumber - 1) * 15);
        BoatGameData.allCoinsCount = money;
        _recoverButtton.SetActive(false);
    }

}
