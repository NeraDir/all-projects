using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RevengerMenuScript : MonoBehaviour
{
    public static int TotalEarnedDetailsRevenge 
    {
        get 
        {
            if (PlayerPrefs.HasKey("SaveKeyTotalEarnedDetailsRevenge"))
                return PlayerPrefs.GetInt("SaveKeyTotalEarnedDetailsRevenge");
            return 500;
        }
        set 
        {
            PlayerPrefs.SetInt("SaveKeyTotalEarnedDetailsRevenge", value);
        }
    }

    public static int RevengeFirstSpawningItemsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("SaveKeyRevengeFirstSpawningItemsCount"))
            {
                return PlayerPrefs.GetInt("SaveKeyRevengeFirstSpawningItemsCount");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("SaveKeyRevengeFirstSpawningItemsCount", value);
        }
    }

    public static string revengerPlayerSettingKey;

    public static int RevengePlayerPlayCount
    {
        get
        {
            if (PlayerPrefs.HasKey("SaveKeyRevengePlayerPlayCount"))
            {
                return PlayerPrefs.GetInt("SaveKeyRevengePlayerPlayCount");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("SaveKeyRevengePlayerPlayCount", value);
        }
    }

    public GameObject revengePlayerFirstEnterBonusPage;

    public GameObject revengePlayInstructionPage;

    public TMP_Text revengeDisplayTotalDetails;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SaveKeyPLayerFirstEnter"))
        {
            revengePlayInstructionPage.SetActive(true);
            revengePlayerFirstEnterBonusPage.SetActive(true);
            PlayerPrefs.SetInt("SaveKeyPLayerFirstEnter", 1);
        }
        revengeDisplayTotalDetails.text = "x" + TotalEarnedDetailsRevenge.ToString("0");
    }

    public void OnClickPlay() 
    {
        SceneManager.LoadScene("RevengerGameScene");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
