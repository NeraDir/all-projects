using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIPage : MonoBehaviour
{

    [SerializeField]
    private GameObject inctructionPage;
    [SerializeField]
    private TMP_Text maxLevelText;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("CanShowapToSceen"))
        {
            PlayerPrefs.DeleteKey("CanShowapToSceen");
        }
        if (!PlayerPrefs.HasKey("CanShowInctruction"))
        {
            PlayerPrefs.SetInt("CanShowInctruction", 1);
            TapInstructionButton();
        }

        maxLevelText.text = "MAX\nLEVEL " + GamePlayController.maxLevel;

        GamePlayController.levelNumber = 1;
    }

    public void TapPlayButton()
    {
        SceneManager.LoadScene("SCENE_GAME");
    }
    public void TapExitButton()
    {
        Application.Quit();
    }
    public void TapInstructionButton()
    {
        inctructionPage.SetActive(true);
    }
}
