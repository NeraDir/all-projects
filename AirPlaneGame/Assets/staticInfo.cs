using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class staticInfo
{
    public static bool music = true;
    public static bool sound = true;
    public static int money = 0;

    public static int bulletLevel = 0;
    public static int[] maxHp = {3,5,7,9,11};
    
    public static float[] shootTime = {5f, 5.5f, 7f, 9f, 12f};

    public static float[] reloadTime = {1f, 0.9f, 0.8f, 0.7f, 0.6f};
    public static float[] speed = {1.8f, 2.2f, 2.5f, 3f, 3.5f};
    public static float[] magnetSize = {0.08f, 0.12f, 0.16f, 0.2f, 0.3f};

}
