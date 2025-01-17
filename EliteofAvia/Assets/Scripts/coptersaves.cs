using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coptersaves : MonoBehaviour
{
    public static int eliteTryingState
    {
        get
        {
            if (PlayerPrefs.HasKey("eliteTryingStatesaves"))
            {
                return PlayerPrefs.GetInt("eliteTryingStatesaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("eliteTryingStatesaves", value);
        }
    }

    public static int eliteBestScore
    {
        get
        {
            if (PlayerPrefs.HasKey("eliteBestScoresaves"))
            {
                return PlayerPrefs.GetInt("eliteBestScoresaves");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("eliteBestScoresaves", value);
        }
    }

    public static string menusceneName;

    public static int eliteLoadtrysCount
    {
        get
        {
            if (PlayerPrefs.HasKey("eliteLoadtrysCountsaevs"))
            {
                return PlayerPrefs.GetInt("eliteLoadtrysCountsaevs");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("eliteLoadtrysCountsaevs", value);
        }
    }
}
