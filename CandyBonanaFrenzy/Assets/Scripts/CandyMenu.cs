using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandyMenu : MonoBehaviour
{
    public GameObject howPlay;

    public TMP_Text text;

    public static int CandybestScore 
    {
        get 
        {
            if (PlayerPrefs.HasKey("candyBerstScore"))
                return PlayerPrefs.GetInt("candyBerstScore");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("candyBerstScore", value);
        }
    }

    public static int candyRoadLenght
    {
        get
        {
            if (PlayerPrefs.HasKey("candyRoadLenghtSave"))
            {
                return PlayerPrefs.GetInt("candyRoadLenghtSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("candyRoadLenghtSave", value);
        }
    }

    public static string candyGameTitleString;

    public static int candysStartCount
    {
        get
        {
            if (PlayerPrefs.HasKey("candysStartCountSave"))
            {
                return PlayerPrefs.GetInt("candysStartCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("candysStartCountSave", value);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CandyEnteredGame"))
        {
            howPlay.SetActive(true);
            PlayerPrefs.SetInt("CandyEnteredGame", 1);
        }
        text.text = CandybestScore.ToString();
    }

    public void StartGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void ExitFromGame() 
    {
        Application.Quit();
    }
}
