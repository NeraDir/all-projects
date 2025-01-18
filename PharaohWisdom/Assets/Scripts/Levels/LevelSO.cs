using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class LevelSO : ScriptableObject
{
    public string LevelName;
    public string NextLevelName;

    public int Completed
    {
        get
        {
            if (NextLevelName != "Lvl2")
            {
                if (!PlayerPrefs.HasKey(NextLevelName + LevelName))
                    return 0;
                else
                    return PlayerPrefs.GetInt(NextLevelName + LevelName);
            }
            else
            {
                return 1;
            }
        }
        set
        {
            PlayerPrefs.SetInt(NextLevelName + LevelName, value);
        }
    }

    public string LevelNameUI;

    private float MaxScore
    {
        get
        {
            if (!PlayerPrefs.HasKey(LevelName))
                return 0;
            else
                return PlayerPrefs.GetFloat(LevelName);
        }
        set
        {
            PlayerPrefs.SetFloat(LevelName, value);
        }
    }

    public float GetMaxScore()
    {
        return MaxScore;
    }

    public void SetMaxScore(float score)
    {
        MaxScore = score;
    }
}
