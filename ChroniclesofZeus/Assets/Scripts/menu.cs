using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class menu : MonoBehaviour
{
    public string playScene;

    public GameObject howToPlayPage;

    public TMP_Text showMaxLevel;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("howToPlayOpenedZeuser"))
        {
            howToPlayPage.SetActive(true);
            PlayerPrefs.SetInt("howToPlayOpenedZeuser", 1);
        }
    }

    private void LateUpdate()
    {
        showMaxLevel.text = "MAX LVL REACHED: " + Skillsuse.maxRechedLevel.ToString();
    }

    public void OnClickGoPlay() 
    {
        SceneManager.LoadScene(playScene);
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
