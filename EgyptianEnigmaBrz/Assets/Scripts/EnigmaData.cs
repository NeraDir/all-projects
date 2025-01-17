using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaData : MonoBehaviour
{
    public string enigmaBufferKey;

    public static int zombieStartLevelNumber
    {
        get
        {
            if (PlayerPrefs.HasKey("zombieStartLevelNumberSave"))
            {
                return PlayerPrefs.GetInt("zombieStartLevelNumberSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("zombieStartLevelNumberSave", value);
        }
    }

    public static int upgradePageCount
    {
        get
        {
            if (PlayerPrefs.HasKey("upgradePageCountSave"))
            {
                return PlayerPrefs.GetInt("upgradePageCountSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("upgradePageCountSave", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
