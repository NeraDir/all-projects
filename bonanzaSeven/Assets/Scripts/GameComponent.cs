using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameComponent : MonoBehaviour
{
    public static int CaramelTreatMaxWave
    {
        get
        {
            if (PlayerPrefs.HasKey("CaramelTratMaxWaveReachValueKey"))
                return PlayerPrefs.GetInt("CaramelTratMaxWaveReachValueKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("CaramelTratMaxWaveReachValueKey", value);
        }
    }
}
