using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamPlayerDataSaver : MonoBehaviour
{
    public static string ramnameKey;

    public static int ramjarsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("ramjarsCountSaveKey"))
            {
                return PlayerPrefs.GetInt("ramjarsCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ramjarsCountSaveKey", value);
        }
    }

    public static int ramjarCrystallsSpeed
    {
        get
        {
            if (PlayerPrefs.HasKey("ramjarCrystallsSpeedSaveKey"))
            {
                return PlayerPrefs.GetInt("ramjarCrystallsSpeedSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("ramjarCrystallsSpeedSaveKey", value);
        }
    }
}
