using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData : MonoBehaviour
{
    public static int bestScore
    {
        get
        {
            if (!PlayerPrefs.HasKey("bestScore"))
                PlayerPrefs.SetInt("bestScore", 0);
            return PlayerPrefs.GetInt("bestScore");
            
        }
        set
        {
            PlayerPrefs.SetInt("bestScore", value);
        }
    }
}
