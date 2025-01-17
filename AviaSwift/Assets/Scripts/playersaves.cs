using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersaves : MonoBehaviour
{
    public static int aviaPlanesBeginSpeed
    {
        get
        {
            if (PlayerPrefs.HasKey("aviaPlanesBeginSpeed"))
            {
                return PlayerPrefs.GetInt("aviaPlanesBeginSpeed");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("aviaPlanesBeginSpeed", value);
        }
    }

    public static string aviEnemiesName;

    public static int aviPlanesCount
    {
        get
        {
            if (PlayerPrefs.HasKey("aviPlanesCount"))
            {
                return PlayerPrefs.GetInt("aviPlanesCount");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("aviPlanesCount", value);
        }
    }
    public static int playerFirstEnter
    {
        get
        {
            if (PlayerPrefs.HasKey("playerFirstEnter"))
            {
                return PlayerPrefs.GetInt("playerFirstEnter");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("playerFirstEnter", value);
        }
    }

}
