using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject HTP;
    public TMP_Text CoinsTXT;

    public static int FirstIn
    {
        get
        {
            if (!PlayerPrefs.HasKey("FirstIn"))
                return 0;

            return PlayerPrefs.GetInt("FirstIn");
        }
        set
        {
            PlayerPrefs.SetInt("FirstIn", value);
        }
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        if(FirstIn == 0)
        {
            HTP.SetActive(true);
            FirstIn = 1;
        }

        RefreshCoinsTXT();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void RefreshCoinsTXT()
    {
        CoinsTXT.text = SaverManager.Coins.ToString();
    }
}
