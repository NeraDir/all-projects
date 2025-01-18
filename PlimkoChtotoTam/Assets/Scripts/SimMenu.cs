using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimMenu : MonoBehaviour
{
    public TMP_Text bestScore;

    public GameObject howToPlay;

    private void Start()
    {
        if (SimSaves.simPlayerFirstEnter != 1)
        {
            howToPlay.SetActive(true);
            SimSaves.simPlayerFirstEnter = 1;
        }

    }

    private void LateUpdate()
    {
        bestScore.text = "X" + SimSaves.simBestScore.ToString("0");
    }

    public void OnClickPlay() 
    {
        SceneManager.LoadScene("SimGame");
    }

    public void OnCLickExit() 
    {
        Application.Quit();
    }
}
