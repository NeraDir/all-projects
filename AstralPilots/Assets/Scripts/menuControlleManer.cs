using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControlleManer : MonoBehaviour
{
    public TMP_Text bestScoreDisplayer;

    public GameObject hp;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("Hp"))
        {
            hp.SetActive(true);
            PlayerPrefs.SetInt("Hp", 1);
        }
        bestScoreDisplayer.text = LPlanerDate.BestScore.ToString();
    }

    public void ClickPlay() 
    {
        SceneManager.LoadScene("Game");
    }

    public void ClickExit() 
    {
        Application.Quit();
    }
}
