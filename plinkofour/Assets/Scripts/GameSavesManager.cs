using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSavesManager : MonoBehaviour
{
    public static int GameCurrentLevelValue
    {
        get
        {
            if (PlayerPrefs.HasKey("PikoDesertTreasureGameCurrentLevelValuesaves"))
            {
                return PlayerPrefs.GetInt("PikoDesertTreasureGameCurrentLevelValuesaves");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("PikoDesertTreasureGameCurrentLevelValuesaves", value);
        }
    }

    public static int SoundMuteState
    {
        get
        {
            if (PlayerPrefs.HasKey("PikoDesertTreasureSoundMuteStatesaves"))
            {
                return PlayerPrefs.GetInt("PikoDesertTreasureSoundMuteStatesaves");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("PikoDesertTreasureSoundMuteStatesaves", value);
        }
    }

    public static int MusicMuteState
    {
        get
        {
            if (PlayerPrefs.HasKey("PikoDesertTreasureMusicMuteStatesaves"))
            {
                return PlayerPrefs.GetInt("PikoDesertTreasureMusicMuteStatesaves");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("PikoDesertTreasureMusicMuteStatesaves", value);
        }
    }

    public static int GameCurrentBallsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("PikoDesertTreasureGameCurrentBallsCountsaves"))
            {
                return PlayerPrefs.GetInt("PikoDesertTreasureGameCurrentBallsCountsaves");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PikoDesertTreasureGameCurrentBallsCountsaves", value);
        }
    }

    public static int GameHowToPlayDisplayerValue
    {
        get
        {
            if (PlayerPrefs.HasKey("PikoDesertTreasureGameHowToPlayDisplayerValuesaves"))
            {
                return PlayerPrefs.GetInt("PikoDesertTreasureGameHowToPlayDisplayerValuesaves");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PikoDesertTreasureGameHowToPlayDisplayerValuesaves", value);
        }
    }

    public static int GameHeartsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("PikoDesertTreasureGameHeartsCountsaves"))
            {
                return PlayerPrefs.GetInt("PikoDesertTreasureGameHeartsCountsaves");
            }
            return 3;
        }
        set
        {
            PlayerPrefs.SetInt("PikoDesertTreasureGameHeartsCountsaves", value);
        }
    }

    public static int GameBestReachLevelValue
    {
        get
        {
            if (PlayerPrefs.HasKey("PikoDesertTreasureGameBestReachLevelValuesaves"))
            {
                return PlayerPrefs.GetInt("PikoDesertTreasureGameBestReachLevelValuesaves");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("PikoDesertTreasureGameBestReachLevelValuesaves", value);
        }
    }

    public static int pikoTreasureGameLaunchTryCount
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

    public static string pikoTreasureGameName;

    public static int pikoTreasureGameWinsCount
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

    public static int GameBestReachScoreValue
    {
        get
        {
            if (PlayerPrefs.HasKey("PikoDesertTreasureGameBestReachScoreValuesaves"))
            {
                return PlayerPrefs.GetInt("PikoDesertTreasureGameBestReachScoreValuesaves");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PikoDesertTreasureGameBestReachScoreValuesaves", value);
        }
    }

    public static int GameCurrentScoreValue
    {
        get
        {
            if (PlayerPrefs.HasKey("PikoDesertTreasureGameCurrentScoreValuesaves"))
            {
                return PlayerPrefs.GetInt("PikoDesertTreasureGameCurrentScoreValuesaves");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PikoDesertTreasureGameCurrentScoreValuesaves", value);
        }
    }
}
