using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandieGameSpawner : MonoBehaviour
{
    public GameObject clickObject;

    public TMP_Text[] showCurrentAccuracy;

    public TMP_Text showCurrentLeftTime;

    public Transform[] positions;

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
        CandieGameSpawner.CurrentAccuracy = CandieGameSpawner.totalValue / CandieGameSpawner.countDestroyedCircles;
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
        timer = CandieGameConfig.TotalTime;
        while (true)
        {
            GameObject tempObject = Instantiate(clickObject, new Vector3(0,0,0), Quaternion.identity, showCurrentAccuracy[0].transform.parent);
            tempObject.transform.position = new Vector3(Random.Range(positions[0].position.x, positions[1].position.x), Random.Range(positions[0].position.y, positions[1].position.y), 0);
            tempObject.transform.SetSiblingIndex(0);
            yield return new WaitForSeconds(CandieGameConfig.TotalTime / CandieGameConfig.countToSpawnCircles);
        }
    }

    public void ClickMenu()
    {

        if (CandieGameConfig.countToSpawnCircles == 600)
        {
            CandiesPlayerDatas.lostHardlvlAccuracy = CurrentAccuracy;
        }
        if (CandieGameConfig.countToSpawnCircles == 450)
        {
            CandiesPlayerDatas.lostMiddlelvlAccuracy = CurrentAccuracy;
        }
        if (CandieGameConfig.countToSpawnCircles == 300)
        {
            CandiesPlayerDatas.lostEasylvlAccuracy = CurrentAccuracy;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ClickRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
