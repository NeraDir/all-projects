using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDatasSaver : MonoBehaviour
{
    public static int spiritNeedSpeedOfCrystalls
    {
        get
        {
            if (PlayerPrefs.HasKey("spiritNeedSpeedOfCrystallsSave"))
            {
                return PlayerPrefs.GetInt("spiritNeedSpeedOfCrystallsSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("spiritNeedSpeedOfCrystallsSave", value);
        }
    }

    public static int countOfPressedNext
    {
        get
        {
            if (PlayerPrefs.HasKey("countOfPressedNext"))
            {
                return PlayerPrefs.GetInt("countOfPressedNext");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("countOfPressedNext", value);
        }
    }

    public static string spiritPlayerName;

    public static int maxresearchedLVl
    {
        get
        {
            if (PlayerPrefs.HasKey("maxresearchedLVl"))
            {
                return PlayerPrefs.GetInt("maxresearchedLVl");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("maxresearchedLVl", value);
        }
    }


    public static int crystallsCountSpawnOnLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("crystallsCountSpawnOnLevel"))
            {
                return PlayerPrefs.GetInt("crystallsCountSpawnOnLevel");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("crystallsCountSpawnOnLevel", value);
        }
    }



    public static int isFirstEnter 
    {
        get 
        {
            if (PlayerPrefs.HasKey("isFirstPlayerEnterTheGame"))
            {
                return PlayerPrefs.GetInt("isFirstPlayerEnterTheGame");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("isFirstPlayerEnterTheGame", value);
        }
    }

    public TMP_Text showMaxLVl;

    private void LateUpdate()
    {
        if (showMaxLVl != null)
        {
            showMaxLVl.text = $"MAX LVL \n<color=#aa00ff>{maxresearchedLVl}</color>";
        }
    }

}
