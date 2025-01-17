using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallConfigsController : MonoBehaviour
{
    public static int coinCount;
    public static int ballHealth;
    public static float currentMettersValue;
    public static float currentMaxMetters;

    public static float mainRecordMetters
    {
        get
        {
            if (PlayerPrefs.HasKey("recordSaveKey"))
            {
                return PlayerPrefs.GetFloat("recordSaveKey");
            }

            PlayerPrefs.SetFloat("recordSaveKey", 0);
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("recordSaveKey", value);
        }
    }




    private void Start()
    {
        coinCount = 0;
        ballHealth = 3;
    }
}
