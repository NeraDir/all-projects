using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveClass : MonoBehaviour
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
            if (MaxScore < value)
            {
                PlayerPrefs.SetInt("MaxScore", value);
            }
        }
    }
}
