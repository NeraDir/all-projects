using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OracleMysteryConfigs : MonoBehaviour
{
    public string OracleMysteryMainKey;

    public static int configID
    {
        get
        {
            if (PlayerPrefs.HasKey("configIDSave"))
            {
                return PlayerPrefs.GetInt("configIDSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("configIDSave", value);
        }
    }

    public static int configCointerValue
    {
        get
        {
            if (PlayerPrefs.HasKey("configCointerValueSave"))
            {
                return PlayerPrefs.GetInt("configCointerValueSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("configCointerValueSave", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
