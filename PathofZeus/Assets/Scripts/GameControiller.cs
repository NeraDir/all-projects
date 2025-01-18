using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControiller : MonoBehaviour
{
    public TMP_Text[] showLivingTime;

    public static float livingTime;

    public static bool isPlay;

    private void Start()
    {
        isPlay = false;
        livingTime = 0;
    }

    private void LateUpdate()
    {
        if (isPlay)
            return;
        livingTime += Time.deltaTime;
        foreach (var item in showLivingTime)
        {
            item.text = "LIVING : " + livingTime.ToString("00.0") +"s";
        }

        if (livingTime > zevsSaves.LivingTimeRecord)
        {
            zevsSaves.LivingTimeRecord = livingTime;
        }
    }

    public void OnCLickMenu() 
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickRestatrt() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
