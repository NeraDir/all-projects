using System;
using UnityEngine;

public class StarsTimeUtilities : MonoBehaviour
{
    public static int SetTime(DateTime dataTime)
    {
        DateTime defDataTime = new DateTime(1970, 1, 1);
        TimeSpan subTime = dataTime.Subtract(defDataTime);

        return (int)subTime.TotalSeconds;
    }

    public static int SetTime()
    {
        return SetTime(DateTime.UtcNow);
    }
}
