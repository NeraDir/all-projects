using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int Money
    {
        get
        {
            if (PlayerPrefs.HasKey("moneySaveKey"))
            {
                return PlayerPrefs.GetInt("moneySaveKey");
            }
            else
            {
                PlayerPrefs.SetInt("moneySaveKey", 100);
                return 100;
            }
        }
        set
        {
            PlayerPrefs.SetInt("moneySaveKey", value);
        }
    }
}
