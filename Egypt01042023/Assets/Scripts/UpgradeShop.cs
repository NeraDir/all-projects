using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    public PlayerMnagaer playerInfo;
    public TMP_Text HealthLevel;
    public TMP_Text AttackLevel;
    public TMP_Text attSpeedLevel;

    public static int HPLevel
    {
        get
        {
            if (!PlayerPrefs.HasKey("HPLevel"))
                return 1;
            else
                return PlayerPrefs.GetInt("HPLevel");
        }
        set
        {
            PlayerPrefs.SetInt("HPLevel", value);
        }
    }

    public static int AttackLevel1
    {
        get
        {
            if (!PlayerPrefs.HasKey("AttackLevel1"))
                return 1;
            else
                return PlayerPrefs.GetInt("AttackLevel1");
        }
        set
        {
            PlayerPrefs.SetInt("AttackLevel1", value);
        }
    }

    public static int attSPLevel
    {
        get
        {
            if (!PlayerPrefs.HasKey("attSPLevel"))
                return 1;
            else
                return PlayerPrefs.GetInt("attSPLevel");
        }
        set
        {
            PlayerPrefs.SetInt("attSPLevel", value);
        }
    }

    private void Start()
    {
        HealthLevel.text = "LEVEL " + HPLevel.ToString();
        AttackLevel.text = "LEVEL " + AttackLevel1.ToString();
        attSpeedLevel.text = "LEVEL " + attSPLevel.ToString();
    }

    public void UpgradeAttackDamage()
    {
        GameManager.DamageMultiplier += 0.1f;
        AttackLevel1++;
        AttackLevel.text = "LEVEL " + AttackLevel1.ToString();
    }

    public void UpgradeAttackSpeed()
    {
        if (playerInfo.attackSpeed - playerInfo.attackSpeed * GameManager.SpeedMultiplier > 0.3f)
        {
            GameManager.SpeedMultiplier += 0.2f;
            attSPLevel++;
            attSpeedLevel.text = "LEVEL " + attSPLevel.ToString();
        }
    }

    public void UpgradeHealth()
    {
        GameManager.HealthMultiplier += 0.2f;
        HPLevel++;
        HealthLevel.text = "LEVEL " + HPLevel.ToString();

        playerInfo.UpdateStats();
    }
}
