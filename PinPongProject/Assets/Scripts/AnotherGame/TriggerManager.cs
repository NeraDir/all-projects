using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public WinControll NextLevel;
    public float TimeToEnd = 10f;

    public int ScoreCount = 0;
    private float Timer = 0f;

    private void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= TimeToEnd)
        {
            NextLevel.Init(ScoreCount, ScoreCount, true);
            NextLevel.gameObject.SetActive(true);
        }
    }
}
