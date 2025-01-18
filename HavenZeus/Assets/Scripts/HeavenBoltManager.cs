using UnityEngine;

public class HeavenBoltManager : MonoBehaviour
{
    [HideInInspector] public string heavenZeusTempString;

    public static int boltSpeed
    {
        get
        {
            if (PlayerPrefs.HasKey("boltSpeedSaveKey"))
            {
                return PlayerPrefs.GetInt("boltSpeedSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("boltSpeedSaveKey", value);
        }
    }

    public static int zeusStrenght
    {
        get
        {
            if (PlayerPrefs.HasKey("zeusStrenghtSaveKey"))
            {
                return PlayerPrefs.GetInt("zeusStrenghtSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("zeusStrenghtSaveKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
