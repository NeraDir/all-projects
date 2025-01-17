using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSave : MonoBehaviour
{
    public static int KnifeIndex
    {
        get
        {
            if (!PlayerPrefs.HasKey("KnifeIndexSave"))
                return 0;
            else
                return PlayerPrefs.GetInt("KnifeIndexSave");
        }
        set
        {
            PlayerPrefs.SetInt("KnifeIndexSave", value);
        }
    }

    public static int CoinsCount
    {
        get
        {
            if (!PlayerPrefs.HasKey("CoinsSAveCount"))
                return 5000;
            else
                return PlayerPrefs.GetInt("CoinsSAveCount");
        }
        set
        {
            PlayerPrefs.SetInt("CoinsSAveCount", value);
        }
    }
}
