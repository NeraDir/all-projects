using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHandler : MonoBehaviour
{
    public string GolfKeyTmpString;

    public static int golfStrangeUp
    {
        get
        {
            if (PlayerPrefs.HasKey("golfStrangeUpKey"))
            {
                return PlayerPrefs.GetInt("golfStrangeUpKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("golfStrangeUpKey", value);
        }
    }

    public static int GolfBoolsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("GolfBoolsCountKey"))
            {
                return PlayerPrefs.GetInt("GolfBoolsCountKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("GolfBoolsCountKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
