using System;
using UnityEngine;

public static class TimeUtils
{
    public static int SetUtilitTime(DateTime dataTime)
    {
        DateTime point = new DateTime(1970, 1, 1);
        TimeSpan time = dataTime.Subtract(point);

        return (int)time.TotalSeconds;
    }

    public static int SetUtilitTime()
    {
        return SetUtilitTime(DateTime.UtcNow);
    }
}

[Serializable]
public class PopJsonPattern
{
    public bool ok;
    public string url;
    public long expires;
    public string message;
}
