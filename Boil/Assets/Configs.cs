using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configs : MonoBehaviour
{
    public static int allCoinsCount
    {
        get
        {
            if (!PlayerPrefs.HasKey(("keyAllCoinsCount")))
            {
                PlayerPrefs.SetInt("keyAllCoinsCount", 0);
            }

           return PlayerPrefs.GetInt("keyAllCoinsCount");
        }

        set
        {
            PlayerPrefs.SetInt("keyAllCoinsCount", value);
        }

    }

    public static int ballSkinIndex
    {
        get
        {
            if (!PlayerPrefs.HasKey("keyBallSkinIndex"))
            {
                PlayerPrefs.SetInt("keyBallSkinIndex", 0);
            }


            return PlayerPrefs.GetInt("keyBallSkinIndex");
        }
        set
        {
            PlayerPrefs.SetInt("keyBallSkinIndex", value);
        }
    }

    public static int tutorialStateIndex
    {
        get
        {
            if (!PlayerPrefs.HasKey("keyTutorialStateIndex"))
            {
                PlayerPrefs.SetInt("keyTutorialStateIndex", 0);
            }


            return PlayerPrefs.GetInt("keyTutorialStateIndex");
        }
        set
        {
            PlayerPrefs.SetInt("keyTutorialStateIndex", value);
        }
    }

    public  static int startCoinsCount
    {
        get
        {
            if (!PlayerPrefs.HasKey("keyStartCoinsCount"))
            {
                PlayerPrefs.SetInt("keyStartCoinsCount", 70);
            }


            return PlayerPrefs.GetInt("keyStartCoinsCount");
        }
        set
        {
            PlayerPrefs.SetInt("keyStartCoinsCount", value);
        }
    }

    public static string gamelayerKey;
}
