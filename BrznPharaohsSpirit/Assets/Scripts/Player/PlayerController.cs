using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TMP_Text HealthTXT;
    public TMP_Text TimerTXT;
    public WinPanel WinPanel;

    [Header("Params")]
    public float TimeToDisactivateCell = 1f;
    public float DisactiveColorSpace = 0f;
    public float Health = 5f;

    float Timer = 0f;
    bool GameActive = false;
    public static string tempCardsCount;
    public static int protectionShieldCount
    {
        get
        {
            if (PlayerPrefs.HasKey("cardCOuntSave"))
            {
                return PlayerPrefs.GetInt("cardCOuntSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("cardCOuntSave", value);
        }
    }

    public static int protectionAramorValue
    {
        get
        {
            if (PlayerPrefs.HasKey("cardTrueCountSave"))
            {
                return PlayerPrefs.GetInt("cardTrueCountSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("cardTrueCountSave", value);
        }
    }

    private void Awake()
    {
        HealthTXT.text = "Health: " + Health.ToString();
        TimerTXT.text = "Time: " + (int)Timer;
        GameActive = true;
    }

    private void Update()
    {
        if(GameActive)
        {
            Timer += Time.deltaTime;
            TimerTXT.text = "Time: " + (int)Timer; 
        }
    }

    public void DecreaseHealth()
    {
        Health -= 1;
        HealthTXT.text = "Health: " + Health.ToString();

        if (Health <= 0)
        {
            //Win
            WinPanel.gameObject.SetActive(true);
            GameActive = false;
            WinPanel.Init((int)Timer);
        }
    }
}
