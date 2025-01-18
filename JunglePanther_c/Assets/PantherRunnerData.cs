using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantherRunnerData : MonoBehaviour
{
    public static int modelIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("modelIndexSave"))
            {
                return PlayerPrefs.GetInt("modelIndexSave");

            }

            PlayerPrefs.SetInt("modelIndexSave", 0);
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("modelIndexSave", value);
        }
    }

    public static int coins
    {
        get
        {
            if (PlayerPrefs.HasKey("coinsSaveKey"))
            {
                return PlayerPrefs.GetInt("coinsSaveKey");

            }

            PlayerPrefs.SetInt("coinsSaveKey", 1000);
            return 10000;
        }
        set
        {
            PlayerPrefs.SetInt("coinsSaveKey", value);
        }
    }
}
