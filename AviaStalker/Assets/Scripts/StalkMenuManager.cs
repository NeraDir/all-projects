using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StalkMenuManager : MonoBehaviour
{
    public GameObject stalkHowToPlayScreen;

    public TMP_Text stalkREpairedPlanesCountShow;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("StalkPlanesRecoveredHowToPlayShoweSavingKey"))
        {
            stalkHowToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("StalkPlanesRecoveredHowToPlayShoweSavingKey", 1);
        }
        stalkREpairedPlanesCountShow.text = StalkGamingManager.stalkPlanesRecoveredScoreBest.ToString();
    }

    public void OnPlayPressed() 
    {
        SceneManager.LoadScene("StalkGamingScene");
    }

    public void OnExitPressed() 
    {
        Application.Quit();
    }
}
