using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static float lifetime;
    public static int score;
    public static float moneys;

    [SerializeField]
    private TMP_Text[] lifetimeShow;

    [SerializeField]
    private TMP_Text[] scoreShow;

    [SerializeField]
    private TMP_Text[] moneysShow;
    

    private void Start()
    {
        lifetime = 0;
        score = 0;
        moneys = 0;
    }

    private void LateUpdate()
    {
        lifetime += Time.deltaTime;

        foreach (var item in moneysShow)
        {
            item.text = moneys.ToString();
        }

        foreach (var item in scoreShow)
        {
            item.text = score.ToString();
        }

        foreach (var item in lifetimeShow)
        {
            item.text = lifetime.ToString("0.0");
        }
    }


    public void OnCLickPause() 
    {
        Time.timeScale = 0;
    }

    public void OnCLickResume() 
    {
        Time.timeScale = 1;
    }

    public void OnClickExit() 
    {
        Application.Quit();
        AviationDataSaveClass.AviationLoveMoneys += moneys;
        Time.timeScale = 1;
    }

    public void OnClickRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AviationDataSaveClass.AviationLoveMoneys += moneys;
        Time.timeScale = 1;
    }

    public void OnClickMenu() 
    {
        SceneManager.LoadScene("Menu");
        AviationDataSaveClass.AviationLoveMoneys += moneys;
        Time.timeScale = 1;
    }
}
