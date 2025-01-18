using UnityEngine;

public class GamePlayerInformation : MonoBehaviour
{
    public static float RecordOfLivingTime 
    {
        get 
        {
            if (PlayerPrefs.HasKey("AircrafterLivingRecordSaveKey"))
            {
                return PlayerPrefs.GetFloat("AircrafterLivingRecordSaveKey");
            }
            return 0f;
        }
        set 
        {
            PlayerPrefs.SetFloat("AircrafterLivingRecordSaveKey", value);
        }
    }

    public static int RecordOfPassedRings
    {
        get
        {
            if (PlayerPrefs.HasKey("AircrafterPassedRingsRecordSaveKey"))
            {
                return PlayerPrefs.GetInt("AircrafterPassedRingsRecordSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("AircrafterPassedRingsRecordSaveKey", value);
        }
    }

    public static float GameCoins
    {
        get
        {
            if (PlayerPrefs.HasKey("AircrafterGameCoinsSaveKey"))
            {
                return PlayerPrefs.GetFloat("AircrafterGameCoinsSaveKey");
            }
            return 150f;
        }
        set
        {
            PlayerPrefs.SetFloat("AircrafterGameCoinsSaveKey", value);
        }
    }

    public static float PlanesSpeed
    {
        get
        {
            if (PlayerPrefs.HasKey("AircrafterPlnaesSpeedSaveKey"))
            {
                return PlayerPrefs.GetFloat("AircrafterPlnaesSpeedSaveKey");
            }
            return 20f;
        }
        set
        {
            PlayerPrefs.SetFloat("AircrafterPlnaesSpeedSaveKey", value);
        }
    }

    public static int PlaneSelected 
    {
        get
        {
            if (PlayerPrefs.HasKey("AircrafterPlaneIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("AircrafterPlaneIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("AircrafterPlaneIndexSaveKey", value);
        }
    }
}
