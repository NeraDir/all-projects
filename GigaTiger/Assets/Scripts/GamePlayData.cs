using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayData : MonoBehaviour
{
    public static int recordstartdata
    {
        get
        {
            if (PlayerPrefs.HasKey("RecordStarsCountKey"))
            {
                PlayerPrefs.SetInt("RecordStarsCountKey", 0);
            }

            return PlayerPrefs.GetInt("RecordStarsCountKey");
        }
        set
        {
            PlayerPrefs.SetInt("RecordStarsCountKey", value);
        }
    }
    public static string howtoplaydata
    {
        get
        {
            if (PlayerPrefs.HasKey("HowToPllayKey"))
            {
                PlayerPrefs.SetString("HowToPllayKey", "false");
            }

            return PlayerPrefs.GetString("HowToPllayKey");
        }
        set
        {
            PlayerPrefs.SetString("HowToPllayKey", value);
        }

    }

    public static int tigerMoveSpeedValue
    {
        get
        {
            if (PlayerPrefs.HasKey("tigerMoveSpeedValueSave"))
            {
                return PlayerPrefs.GetInt("tigerMoveSpeedValueSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("tigerMoveSpeedValueSave", value);
        }
    }

    public static string tigerLoadSceneName;

    public static int tigerPlatformWithHoles
    {
        get
        {
            if (PlayerPrefs.HasKey("tigerPlatformWithHolesSave"))
            {
                return PlayerPrefs.GetInt("tigerPlatformWithHolesSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("tigerPlatformWithHolesSave", value);
        }
    }
}
