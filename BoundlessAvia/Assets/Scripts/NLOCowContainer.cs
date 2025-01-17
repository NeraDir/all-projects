using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NLOCowContainer : MonoBehaviour
{
   [HideInInspector] public string cowCatchTemp;

    public static int CowSavingValue
    {
        get
        {
            if (PlayerPrefs.HasKey("CowSavingValueSavingValueKey"))
            {
                return PlayerPrefs.GetInt("CowSavingValueSavingValueKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("CowSavingValueSavingValueKey", value);
        }
    }

    public static int CowCatchCount
    {
        get
        {
            if (PlayerPrefs.HasKey("CowCatchCountSaveKey"))
            {
                return PlayerPrefs.GetInt("CowCatchCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("CowCatchCountSaveKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
