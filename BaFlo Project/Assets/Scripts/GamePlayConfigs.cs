using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayConfigs : MonoBehaviour
{
    public static int levelNumber
    {
        get
        {
            if (PlayerPrefs.HasKey("levelNumberSave"))
            {
                return PlayerPrefs.GetInt("levelNumberSave");
            }

            PlayerPrefs.SetInt("levelNumberSave", 1);
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("levelNumberSave", value);
        }
    }

    public static int coinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("coinsCountSave"))
            {
                return PlayerPrefs.GetInt("coinsCountSave");
            }

            PlayerPrefs.SetInt("coinsCountSave", 0);
            return 100;
        }
        set
        {
            PlayerPrefs.SetInt("coinsCountSave", value);
        }
    }

    public static int healthLevelNumber
    {
        get
        {
            if (PlayerPrefs.HasKey("healthLevelNumberSave"))
            {
                return PlayerPrefs.GetInt("healthLevelNumberSave");
            }

            PlayerPrefs.SetInt("healthLevelNumberSave", 1);
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("healthLevelNumberSave", value);


        }
    }

    public static int energyLevelNumber
    {
        get
        {
            if (PlayerPrefs.HasKey("energyLevelNumberSave"))
            {
                return PlayerPrefs.GetInt("energyLevelNumberSave");
            }

            PlayerPrefs.SetInt("energyLevelNumberSave", 1);
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("energyLevelNumberSave", value);


        }
    }
    public static int damageLevelNumber
    {
        get
        {
            if (PlayerPrefs.HasKey("damageLevelNumberSave"))
            {
                return PlayerPrefs.GetInt("damageLevelNumberSave");
            }

            PlayerPrefs.SetInt("damageLevelNumberSave", 1);
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("damageLevelNumberSave", value);


        }
    }

}
