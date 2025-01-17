using UnityEngine;

public class ConfigMoveComponent : MonoBehaviour
{
    public string fairyConfigString;

    public static int musicVolumeValue
    {
        get
        {
            if (PlayerPrefs.HasKey("musicVolumeValueSaveKey"))
            {
                return PlayerPrefs.GetInt("musicVolumeValueSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("musicVolumeValueSaveKey", value);
        }
    }

    public static int BoatMovingSpeedValue
    {
        get
        {
            if (PlayerPrefs.HasKey("BoatMovingSpeedValueSaveKey"))
            {
                return PlayerPrefs.GetInt("BoatMovingSpeedValueSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("BoatMovingSpeedValueSaveKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
