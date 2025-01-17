using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSavesData : MonoBehaviour
{
    public static int PlayerGCoinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("CrazyTinesPlayerGCoinsCountSaveKey"))
            {
                return PlayerPrefs.GetInt("CrazyTinesPlayerGCoinsCountSaveKey");
            }
            return 400;
        }
        set
        {
            PlayerPrefs.SetInt("CrazyTinesPlayerGCoinsCountSaveKey", value);
        }
    }

    public static int SelectedLevelIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("CrazyTinesSelectedLevelIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("CrazyTinesSelectedLevelIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("CrazyTinesSelectedLevelIndexSaveKey", value);
        }
    }

    public static int SelectedBgIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("CrazyTinesSelectedBGIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("CrazyTinesSelectedBGIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("CrazyTinesSelectedBGIndexSaveKey", value);
        }
    }

    public static int MaxReachLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("CrazyTinesMaxReachLevelIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("CrazyTinesMaxReachLevelIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("CrazyTinesMaxReachLevelIndexSaveKey", value);
        }
    }
}
