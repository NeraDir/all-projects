using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotHandledParams : MonoBehaviour
{
    public static string stringClouded;

    public static int FirstCall
    {
        get
        {
            if (PlayerPrefs.HasKey("FirstCallKey"))
            {
                return PlayerPrefs.GetInt("FirstCallKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("FirstCallKey", value);
        }
    }

    public static int SecondCall
    {
        get
        {
            if (PlayerPrefs.HasKey("SecondCallKey"))
            {
                return PlayerPrefs.GetInt("SecondCallKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("SecondCallKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
