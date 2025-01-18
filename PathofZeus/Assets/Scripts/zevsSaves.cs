using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zevsSaves : MonoBehaviour
{
    public static float LivingTimeRecord 
    {
        get 
        {
            if (PlayerPrefs.HasKey("ZevsSaveLivingTimeRecord"))
            {
                return PlayerPrefs.GetFloat("ZevsSaveLivingTimeRecord");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetFloat("ZevsSaveLivingTimeRecord", value);
        } 
    }

    public static int ZevsMovementSpeed
    {
        get
        {
            if (PlayerPrefs.HasKey("ZevsMovementSpeedKery"))
                return PlayerPrefs.GetInt("ZevsMovementSpeedKery");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ZevsMovementSpeedKery", value);
        }
    }

    public static int ZevsCanvasScaleValue
    {
        get
        {
            if (PlayerPrefs.HasKey("ZevsCanvasScaleValueKey"))
                return PlayerPrefs.GetInt("ZevsCanvasScaleValueKey");
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("ZevsCanvasScaleValueKey", value);
        }
    }

    public static string zevsNameString;
}
