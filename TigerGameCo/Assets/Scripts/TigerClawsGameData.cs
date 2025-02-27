using UnityEngine;

public class TigerClawsGameData
{
    public static int TigerClawsSelectedBackgroundIndex
    {
        get => PlayerPrefs.GetInt("TigerClawsSelectedBackgroundIndexSaveKey", 0);
        set => PlayerPrefs.SetInt("TigerClawsSelectedBackgroundIndexSaveKey", value);
    }

    public static int TigerClawsUserCoins
    {
        get => PlayerPrefs.GetInt("TigerClawsUserCoinsSaveKey", 0);
        set => PlayerPrefs.SetInt("TigerClawsUserCoinsSaveKey", value);
    }

    public static int TigerClawsMaxReachedLevels
    {
        get => PlayerPrefs.GetInt("TigerClawsMaxReachedLevelsSaveKey", 0);
        set => PlayerPrefs.SetInt("TigerClawsMaxReachedLevelsSaveKey", value);
    }

    public static int TigerClawsMCurentLevel
    {
        get => PlayerPrefs.GetInt("TigerClawsMCurentLevelSaveKey", 0);
        set => PlayerPrefs.SetInt("TigerClawsMCurentLevelSaveKey", value);
    }

    public static bool TigerClawsFirstEntry
    {
        get => bool.Parse(PlayerPrefs.GetString("TigerClawsFirstEntrySaveKey", "false"));
        set => PlayerPrefs.SetString("TigerClawsFirstEntrySaveKey", value.ToString());
    }

}
