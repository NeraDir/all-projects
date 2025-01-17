using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrzingMovementer : MonoBehaviour
{
    public string BrzingMovementeIdenteficator;

    public static string brzingCureentTempString;
    public static string brzingIdfaSaiKey;

    public static int brzingCureentTempInt 
    {
        get 
        {
            if (PlayerPrefs.HasKey("toolbaringShowSaveKey"))
            {
                return PlayerPrefs.GetInt("toolbaringShowSaveKey");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("toolbaringShowSaveKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
