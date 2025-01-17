using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_MenuLayer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text recordLevelTMPUI;
    [SerializeField]
    private GameObject HowToPlayPageLayer;


    private void Start()
    {
        CheckPlay();
        recordLevelTMPUI.text = "RECORD\nLEVEL\n" + ParametersPerformer.recordLevel;
    }


    public void PlayButtonFunction()
    {
        SceneManager.LoadScene("JellyPeaks_MENU_GAME_LEVEL_1");
    }
    public void ExitButtonFunction()
    {
        Application.Quit();
    }
    public void HowToPlayButtonFunction()
    {
        OpenHowToPlay();
    }

    private void OpenHowToPlay()
    {
        HowToPlayPageLayer.SetActive(true);
    }
    private void CheckPlay()
    {
        if (!PlayerPrefs.HasKey("PlayerPrefsKeyHowToPlay"))
        {
            StartCoroutine(showHowToPLay());
            PlayerPrefs.SetString("PlayerPrefsKeyHowToPlay", "howtoplay");
        }
    }
    private IEnumerator showHowToPLay()
    {
        yield return new WaitForSeconds(3);
        OpenHowToPlay();
    }

}
