using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverManager : MonoBehaviour
{
    public static int Coins
    {
        get
        {
            if (!PlayerPrefs.HasKey("Coins"))
                return 0;

            return PlayerPrefs.GetInt("Coins");
        }
        set
        {
            PlayerPrefs.SetInt("Coins", value);
        }
    }

    public static int BoomCount
    {
        get
        {
            if (!PlayerPrefs.HasKey("BoomCount"))
                return 2;

            return PlayerPrefs.GetInt("BoomCount");
        }
        set
        {
            PlayerPrefs.SetInt("BoomCount", value);
        }
    }
    public static int FireworkCount
    {
        get
        {
            if (!PlayerPrefs.HasKey("FireworkCount"))
                return 2;

            return PlayerPrefs.GetInt("FireworkCount");
        }
        set
        {
            PlayerPrefs.SetInt("FireworkCount", value);
        }
    }
    public static int ColorCount
    {
        get
        {
            if (!PlayerPrefs.HasKey("ColorCount"))
                return 2;

            return PlayerPrefs.GetInt("ColorCount");
        }
        set
        {
            PlayerPrefs.SetInt("ColorCount", value);
        }
    }
}
