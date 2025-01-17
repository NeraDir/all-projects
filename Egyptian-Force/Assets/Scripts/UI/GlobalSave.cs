using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalSave : MonoBehaviour
{
    public static int MaxScore
    {
        get
        {
            if (!PlayerPrefs.HasKey("MaxScore"))
                return 0;
            else
                return PlayerPrefs.GetInt("MaxScore");
        }
        set
        {
            if(MaxScore < value)
            {
                PlayerPrefs.SetInt("MaxScore", value);
            }
        }
    }

    public static int Level
    {
        get
        {
            if (!PlayerPrefs.HasKey("LevelSaveKey"))
                return 0;
            else
                return PlayerPrefs.GetInt("LevelSaveKey");
        }
        set
        {
            PlayerPrefs.SetInt("LevelSaveKey", value);
        }
    }

    public static int MaxLevel
    {
        get
        {
            if (!PlayerPrefs.HasKey("MaxLVLSave"))
                return 1;
            else
                return PlayerPrefs.GetInt("MaxLVLSave");
        }
        set
        {
            PlayerPrefs.SetInt("MaxLVLSave", value);
        }
    }
}
