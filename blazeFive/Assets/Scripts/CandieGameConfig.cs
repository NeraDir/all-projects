using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandieGameConfig : MonoBehaviour
{
    public static int countToSpawnCircles;

    public static float TotalTime;

    public static float clickObjectSpeedValueChanger;

    public static float clickObjectChangeColorSpeed;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
