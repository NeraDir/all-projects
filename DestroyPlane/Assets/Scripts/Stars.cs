using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public static int points
    {
        get
        {
            if (PlayerPrefs.HasKey("pointsSK"))
            {
                return PlayerPrefs.GetInt("pointsSK");
            }

            PlayerPrefs.SetInt("pointsSK", 0);
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("pointsSK", value);
        }
    }
}
