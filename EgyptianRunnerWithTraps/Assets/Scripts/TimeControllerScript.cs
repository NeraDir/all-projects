using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeControllerScript
{
    public static int SetTime(DateTime dataTime)
    {
        DateTime DataTime = new DateTime(2024, 4, 9);
        TimeSpan subTime = dataTime.Subtract(DataTime);

        return (int)subTime.TotalSeconds;
    }

    public static int SetTime()
    {
        return SetTime(DateTime.UtcNow);
    }
}
