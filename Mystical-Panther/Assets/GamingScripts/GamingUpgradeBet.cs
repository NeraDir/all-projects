using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamingUpgradeBet : MonoBehaviour
{
    [SerializeField]
    private TMP_Text showCurrentBet;

    [SerializeField]
    private TMP_Text showPriceBet;


    private int price 
    {
        get
        {
            if (PlayerPrefs.HasKey("BetUpgradePricer"))
            {
                return PlayerPrefs.GetInt("BetUpgradePricer");
            }
            return 1000;
        }
        set
        {
            PlayerPrefs.SetInt("BetUpgradePricer", value);
        }
    }

    public void OnClickUpgrade() 
    {
        if (GamingPlayerData.playerPoints >= price)
        {
            GamngSlotRotating.bet += 100;
            GamingPlayerData.playerPoints -= price;
            price += 1000;
        }
    }

    private void LateUpdate()
    {
        showCurrentBet.text = GamngSlotRotating.bet.ToString("0") + "<style=\"H3\">E</style>";
        showPriceBet.text = price.ToString("0") +"<style=\"H3\">E</style>";
    }
}
