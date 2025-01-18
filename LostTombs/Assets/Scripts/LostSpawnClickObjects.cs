using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostSpawnClickObjects : MonoBehaviour
{
    public GameObject clickObject;

    public TMP_Text[] showCurrentAccuracy;

    public TMP_Text showCurrentLeftTime;

    public float timer = 0;

    public static int CurrentAccuracy;

    public GameObject resultPanel;

    public static int countDestroyedCircles = 1;

    public static int totalValue;

    public GameObject loosePanel;

    public static int comboCount;

    public TMP_Text[] showCombos;

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) 
        {
            Time.timeScale = 0;
            resultPanel.SetActive(true);
        }
        else if (countDestroyedCircles > 1 && CurrentAccuracy <= 0)
        {
            Time.timeScale = 0;
            loosePanel.SetActive(true);
        }

        foreach (var item in showCombos)
        {
            item.text = "X" + comboCount.ToString();
        }
        LostSpawnClickObjects.CurrentAccuracy = LostSpawnClickObjects.totalValue / LostSpawnClickObjects.countDestroyedCircles;
        if (CurrentAccuracy <= 0)
        {
            CurrentAccuracy = 0;
        }
        foreach (var item in showCurrentAccuracy)
        {
            item.text = CurrentAccuracy.ToString() + "%";
        }
        showCurrentLeftTime.text = timer.ToString("0") + "s";
    }

    private IEnumerator Start()
    {
        Time.timeScale = 1;
        timer = LostGameConfig.TotalTime;
        while (true)
        {
            Instantiate(clickObject, new Vector3(Random.Range(-1.85f, 1.85f), Random.Range(1.8f, -4.02f), 0), Quaternion.identity);
            yield return new WaitForSeconds(LostGameConfig.TotalTime / LostGameConfig.countToSpawnCircles);
        }
    }

    public void ClickMenu()
    {
      
        if (LostGameConfig.countToSpawnCircles == 600)
        {
            LostGamePlayerSaves.lostHardlvlAccuracy = CurrentAccuracy;
        }
        if (LostGameConfig.countToSpawnCircles == 450)
        {
            LostGamePlayerSaves.lostMiddlelvlAccuracy = CurrentAccuracy;
        }
        if (LostGameConfig.countToSpawnCircles == 300)
        {
            LostGamePlayerSaves.lostEasylvlAccuracy = CurrentAccuracy;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Mneu");
    }

    public void ClickRestart() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
