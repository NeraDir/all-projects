using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamingUpgradeTimer : MonoBehaviour
{

    [SerializeField]
    private TMP_Text showTimerValue;

    [SerializeField]
    private TMP_Text showPriceTimer;

    private int price
    {
        get
        {
            if (PlayerPrefs.HasKey("TimerUpgradePricer"))
            {
                return PlayerPrefs.GetInt("TimerUpgradePricer");
            }
            return 100;
        }
        set
        {
            PlayerPrefs.SetInt("TimerUpgradePricer", value);
        }
    }

    public void OnClickUpgrade()
    {
        if (GamingPlayerData.playerPoints >= price)
        {
            GamingMenuSceneManager.startingTime += 1;
            GamingPlayerData.playerPoints -= price;
            price += 100;
        }
    }

    private void LateUpdate()
    {
        showTimerValue.text = GamingMenuSceneManager.startingTime.ToString("0") + "<style=\"H3\">s</style>";
        showPriceTimer.text = price.ToString("0") + "<style=\"H3\">E</style>";
    }
}
