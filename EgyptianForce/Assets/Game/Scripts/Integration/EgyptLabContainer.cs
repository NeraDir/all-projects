using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgyptLabContainer : MonoBehaviour
{
    [HideInInspector]public string egyptLabTempStrings;

    public static int LabirintStrenth
    {
        get
        {
            if (PlayerPrefs.HasKey("LabirintStrenthSaveKey"))
            {
                return PlayerPrefs.GetInt("LabirintStrenthSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("LabirintStrenthSaveKey", value);
        }
    }

    public static int LabirintValueses
    {
        get
        {
            if (PlayerPrefs.HasKey("LabirintValuesesSaveKey"))
            {
                return PlayerPrefs.GetInt("LabirintValuesesSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("LabirintValuesesSaveKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
