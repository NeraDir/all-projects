using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeHelper : MonoBehaviour
{
    public bool light;
    public bool middle;
    public bool hard;
    public bool life;

    public GameObject[] piecesOfUpgrade;

    [SerializeField] private TMP_Text _showPrice;
    [SerializeField] private TMP_Text _showValue;

    public int Pricer
    {
        get
        {
            if (PlayerPrefs.HasKey("upgradePricer" + gameObject.name))
            {
                return PlayerPrefs.GetInt("upgradePricer" + gameObject.name);
            }
            return 5;
        }
        set
        {
            PlayerPrefs.SetInt("upgradePricer" + gameObject.name, value);
        }
    }

    public int Stack
    {
        get
        {
            if (PlayerPrefs.HasKey("upgradeStacker" + gameObject.name))
            {
                return PlayerPrefs.GetInt("upgradeStacker" + gameObject.name);
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("upgradeStacker" + gameObject.name, value);
        }
    }

    public void ClickUpgrade() 
    {
        if (MenuHelper.PlayerCoins >= Pricer && Stack < piecesOfUpgrade.Length)
        {
            if (light)
                PlayerController.lightDamage++;
            if (middle)
                PlayerController.MediumDamage++;
            if (hard)
                PlayerController.HardDamage++;
            if (life)
                EnemyController.EnemyDamage -= 0.01f;

            MenuHelper.PlayerCoins -= Pricer;
            Stack++;
            Pricer += 5;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < piecesOfUpgrade.Length; i++)
        {
            if (i < Stack)
            {
                piecesOfUpgrade[i].SetActive(true);
            }
            else
            {
                piecesOfUpgrade[i].SetActive(false);
            }
        }

        _showPrice.text = Pricer.ToString("0");
    }
}

