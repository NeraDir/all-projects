using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class BoatGameData
{
    public static int allCoinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("allCoinsCountSaveKey"))
            {
                return PlayerPrefs.GetInt("allCoinsCountSaveKey");
            }
            else
            {
                PlayerPrefs.SetInt("allCoinsCountSaveKey", 1000);
                return 1000;
            }
        }
        set
        {
            PlayerPrefs.SetInt("allCoinsCountSaveKey", value);
        }
    }

    public static int boatSpeedLevelNumber
    {

        get
        {
            if (PlayerPrefs.HasKey("boatSpeedLevelNumberSaveKey"))
            {
                return PlayerPrefs.GetInt("boatSpeedLevelNumberSaveKey");
            }
            else
            {
                PlayerPrefs.SetInt("boatSpeedLevelNumberSaveKey", 1);
                return 1;
            }
        }
        set
        {
            PlayerPrefs.SetInt("boatSpeedLevelNumberSaveKey", value);
        }



    }
    public static int gameTimeLevelNumber
    {
        get
        {
            if (PlayerPrefs.HasKey("gameTimeLevelNumberSaveKey"))
            {
                return PlayerPrefs.GetInt("gameTimeLevelNumberSaveKey");
            }
            else
            {
                PlayerPrefs.SetInt("gameTimeLevelNumberSaveKey", 1);
                return 1;
            }
        }
        set
        {
            PlayerPrefs.SetInt("gameTimeLevelNumberSaveKey", value);
        }
    }
    public static int betValueLevelNumber
    {
        get
        {
            if (PlayerPrefs.HasKey("betValueLevelNumberSaveKey"))
            {
                return PlayerPrefs.GetInt("betValueLevelNumberSaveKey");
            }
            else
            {
                PlayerPrefs.SetInt("betValueLevelNumberSaveKey", 1);
                return 1;
            }
        }
        set
        {
            PlayerPrefs.SetInt("betValueLevelNumberSaveKey", value);
        }
    }


   


    public void Laod()
    {

    }

    public void Save()
    {

    }

}
