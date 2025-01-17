using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgyptAspaScript : MonoBehaviour
{
    public string egyptIDficator;

    public static string bufferEgyptString;
    public static string EgyptRelKey;

    public static int EgyptRelBufferInt 
    {
        get 
        {
            if (PlayerPrefs.HasKey("SnowEgyptDataKey"))
            {
                return PlayerPrefs.GetInt("SnowEgyptDataKey");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("SnowEgyptDataKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
