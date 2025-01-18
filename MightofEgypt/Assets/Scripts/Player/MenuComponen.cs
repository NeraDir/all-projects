using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuComponen : MonoBehaviour
{
    public string menuName;

    public static int menuLoadingIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("menuLoadingIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("menuLoadingIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("menuLoadingIndexSaveKey", value);
        }
    }

    public static int menuLoadingTime
    {
        get
        {
            if (PlayerPrefs.HasKey("menuLoadingTimeSavingKey"))
            {
                return PlayerPrefs.GetInt("menuLoadingTimeSavingKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("menuLoadingTimeSavingKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
