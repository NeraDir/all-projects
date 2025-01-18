using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDatasSaveComponent : MonoBehaviour
{
    public static int MaxReachedLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("JokersMaxReachedLevelSaveKey"))
                return PlayerPrefs.GetInt("JokersMaxReachedLevelSaveKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("JokersMaxReachedLevelSaveKey", value);
        }
    }

    public static int currentCardsPack 
    {
        get
        {
            if (PlayerPrefs.HasKey("JokersCurrentCardsPackSaveKey"))
                return PlayerPrefs.GetInt("JokersCurrentCardsPackSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("JokersCurrentCardsPackSaveKey", value);
        }
    }

    public static int currentLevel 
    {
        get
        {
            if (PlayerPrefs.HasKey("JokersCurrentLevelSaveKey"))
                return PlayerPrefs.GetInt("JokersCurrentLevelSaveKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("JokersCurrentLevelSaveKey", value);
        }
    }

    public static string PlayerName;

    public static int SetTime(DateTime dataTime)
    {
        DateTime DataTime = new DateTime(2024, 4, 18);
        TimeSpan subTime = dataTime.Subtract(DataTime);

        return (int)subTime.TotalSeconds;
    }

    public static int SetTime()
    {
        return SetTime(DateTime.UtcNow);
    }
}
