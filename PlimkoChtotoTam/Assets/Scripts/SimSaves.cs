using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimSaves : MonoBehaviour
{
    public static int simBallsSpawnSets
    {
        get
        {
            if (PlayerPrefs.HasKey("simBallsSpawnSetsSaveKey"))
            {
                return PlayerPrefs.GetInt("simBallsSpawnSetsSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("simBallsSpawnSetsSaveKey", value);
        }
    }

    public static string simPlayerName;

    public static int simPlayerFirstEnter
    {
        get
        {
            if (PlayerPrefs.HasKey("simPlayerFirstEnterSaveKey"))
            {
                return PlayerPrefs.GetInt("simPlayerFirstEnterSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("simPlayerFirstEnterSaveKey", value);
        }
    }

    public static int simPlayerCoinsCoint
    {
        get
        {
            if (PlayerPrefs.HasKey("simPlayerCoinsCointSaveKey"))
            {
                return PlayerPrefs.GetInt("simPlayerCoinsCointSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("simPlayerCoinsCointSaveKey", value);
        }
    }


    public static int simCurrentScore;

    public static int simBestScore 
    {
        get
        {
            if (PlayerPrefs.HasKey("simBestScoreSaveKey"))
            {
                return PlayerPrefs.GetInt("simBestScoreSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("simBestScoreSaveKey", value);
        }
    }
}
