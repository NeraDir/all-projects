using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisUserData : MonoBehaviour
{
    private const string COINS_SAVE_KEY = "anubis_user_coins";
    private const string LEVEL_SAVE_KEY = "anubis_current_level";
    private const string BACKGROUND_NAME_SAVE_KEY = "anubis_current_background_name";
    private const string BEST_SCORE_SAVE_KEY = "anubis_best_score";

    public static int BestScore
    {
        get => PlayerPrefs.HasKey(BEST_SCORE_SAVE_KEY) ? PlayerPrefs.GetInt(BEST_SCORE_SAVE_KEY) : 0;
        set => PlayerPrefs.SetInt(BEST_SCORE_SAVE_KEY, value);
    }

    public static int Coins
    {
        get => PlayerPrefs.HasKey(COINS_SAVE_KEY) ? PlayerPrefs.GetInt(COINS_SAVE_KEY) : 0;
        set => PlayerPrefs.SetInt(COINS_SAVE_KEY, value);
    }

    public static int CurrentLevel
    {
        get => PlayerPrefs.HasKey(LEVEL_SAVE_KEY) ? PlayerPrefs.GetInt(LEVEL_SAVE_KEY) : 0;
        set => PlayerPrefs.SetInt(LEVEL_SAVE_KEY, value);
    }

    public static string CurrentBackgroundName
    {
        get => PlayerPrefs.HasKey(BACKGROUND_NAME_SAVE_KEY) ? PlayerPrefs.GetString(BACKGROUND_NAME_SAVE_KEY) : "1";
        set => PlayerPrefs.SetString(BACKGROUND_NAME_SAVE_KEY, value);
    }
}
