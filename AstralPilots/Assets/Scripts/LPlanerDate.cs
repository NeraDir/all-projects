using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPlanerDate : MonoBehaviour
{
    public static float score 
    {
        get 
        {
            if (PlayerPrefs.HasKey("PlanerMathConre"))
                return PlayerPrefs.GetFloat("PlanerMathConre");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetFloat("PlanerMathConre", value);
        }
    }

    public static float BestScore
    {
        get
        {
            if (PlayerPrefs.HasKey("BestScore"))
                return PlayerPrefs.GetFloat("BestScore");
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("BestScore", value);
        }
    }

    public static int planesMathHerarts 
    {
        get
        {
            if (PlayerPrefs.HasKey("planesMathHerarts"))
                return PlayerPrefs.GetInt("planesMathHerarts");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("planesMathHerarts", value);
        }
    }

    public static int PlanesMovingSpeeder
    {
        get
        {
            if (PlayerPrefs.HasKey("PlanesMovingSpeeder"))
                return PlayerPrefs.GetInt("PlanesMovingSpeeder");
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("PlanesMovingSpeeder", value);
        }
    }

    public static string planerName;
}
