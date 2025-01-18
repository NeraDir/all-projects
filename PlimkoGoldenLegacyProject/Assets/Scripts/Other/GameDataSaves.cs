using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSaves : MonoBehaviour
{
    public static string PlayerFirstEnteredInGame
    {
        get
        {
            if (PlayerPrefs.HasKey("waggonPlayerEnteredSaveKey"))
            {
                return PlayerPrefs.GetString("waggonPlayerEnteredSaveKey");
            }
            return "false";
        }
        set
        {
            PlayerPrefs.SetString("waggonPlayerEnteredSaveKey", value);
        }
    }
    public static int PlayerBestScoreValue
    {
        get
        {
            if (PlayerPrefs.HasKey("waggonPlayerBestScoreValueSaveKey"))
            {
                return PlayerPrefs.GetInt("waggonPlayerBestScoreValueSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("waggonPlayerBestScoreValueSaveKey", value);
        }
    }

    public static int pantherTryCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("pantherTryCountssaves"))
            {
                return PlayerPrefs.GetInt("pantherTryCountssaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("pantherTryCountssaves", value);
        }
    }

    public static string panthermathName;

    public static int pantherMathWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("pantherMathWinsCountSave"))
            {
                return PlayerPrefs.GetInt("pantherMathWinsCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("pantherMathWinsCountSave", value);
        }
    }
}
