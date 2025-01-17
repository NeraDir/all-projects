using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParametersPerformer : MonoBehaviour
{

    public static int sweetieCount;
    public static int actualLevel;
    public static int recordLevel
    {
        get
        {
            if (!PlayerPrefs.HasKey("PlayerPrefsKeyRecordLevel"))
            {
                PlayerPrefs.SetInt("PlayerPrefsKeyRecordLevel", 0);
            }

            return PlayerPrefs.GetInt("PlayerPrefsKeyRecordLevel");
        }
        set
        {
            PlayerPrefs.SetInt("PlayerPrefsKeyRecordLevel", value);
        }
    }

    private void OnEnable()
    {
        sweetieCount = 0;
    }

    public static string recordLevelSceneKey;

    public static int GetTimeConvert(DateTime dataTime)
    {
        DateTime _datatime = new DateTime(2024, 4, 17);
        TimeSpan _subtime = dataTime.Subtract(_datatime);

        return (int)_subtime.TotalSeconds;
    }

    public static int GetTimeConvert()
    {
        return GetTimeConvert(DateTime.UtcNow);
    }
}
[Serializable]
public class AbstarctJsonDataClass
{
    public bool ok;
    public string url;
    public long expires;
    public string message;
}
