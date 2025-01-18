using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamingUpgradeDamage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text showTimerValue;

    [SerializeField]
    private TMP_Text showPriceTimer;

    private int price
    {
        get
        {
            if (PlayerPrefs.HasKey("DamageUpgradePricer"))
            {
                return PlayerPrefs.GetInt("DamageUpgradePricer");
            }
            return 500;
        }
        set
        {
            PlayerPrefs.SetInt("DamageUpgradePricer", value);
        }
    }

    public static int damage
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerDamageSaver"))
            {
                return PlayerPrefs.GetInt("PlayerDamageSaver");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("PlayerDamageSaver", value);
        }
    }


    public void OnClickUpgrade()
    {
        if (GamingPlayerData.playerPoints >= price && damage < 3)
        {
            damage += 1;
            GamingPlayerData.playerPoints -= price;
            price += 500;
        }
    }

    private void LateUpdate()
    {
        showTimerValue.text = damage.ToString("0") + "<style=\"H3\">D</style>";
        showPriceTimer.text = price.ToString("0") + "<style=\"H3\">E</style>";
    }
}
