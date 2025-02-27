using System;
using UnityEngine;

public class TlineGameDataSaves : MonoBehaviour
{
    public static int TlineMaxReachedLevel
    {
        get => PlayerPrefs.GetInt("TlineMaxReachedLevelSaveKey", 0);
        set => PlayerPrefs.SetInt("TlineMaxReachedLevelSaveKey", value);
    }

    public static int TlineCurrentLevel
    {
        get => PlayerPrefs.GetInt("TlineCurrentLevelSaveKey", 0);
        set => PlayerPrefs.SetInt("TlineCurrentLevelSaveKey", value);
    }

    public static int TlineCurrentBackgroundIndex
    {
        get => PlayerPrefs.GetInt("TlineCurrentBackgroundSaveKey", 0);
        set => PlayerPrefs.SetInt("TlineCurrentBackgroundSaveKey", value);
    }

    public static int TlineCoins
    {
        get => PlayerPrefs.GetInt("TlineCoinsSaveKey", 0);
        set => PlayerPrefs.SetInt("TlineCoinsSaveKey", value);
    }

    public static DateTime? TlineLastClaimedDailyBonus
    {
        get => PlayerPrefs.HasKey("TlineLastClaimedDailyBonusSaveKey") ? DateTime.Parse(PlayerPrefs.GetString("TlineLastClaimedDailyBonusSaveKey")) : null;
        set => PlayerPrefs.SetString("TlineLastClaimedDailyBonusSaveKey", value.ToString());
    }
}
