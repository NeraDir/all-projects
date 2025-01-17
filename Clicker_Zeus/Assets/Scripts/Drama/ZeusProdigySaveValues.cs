using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusProdigySaveValues : MonoBehaviour
{
    public static string dataoad;

    public static int SaveFF
    {
        get
        {
            if (PlayerPrefs.HasKey("SaveFFValue"))
            {
                return PlayerPrefs.GetInt("SaveFFValue");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("SaveFFValue", value);
        }
    }

    public static int SaveSS
    {
        get
        {
            if (PlayerPrefs.HasKey("SaveSSValue"))
            {
                return PlayerPrefs.GetInt("SaveSSValue");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("SaveSSValue", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
