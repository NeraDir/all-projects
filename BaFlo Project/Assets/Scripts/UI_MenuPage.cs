using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_MenuPage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelNumberText;

    [SerializeField]
    private UI_UpgradePage uI_UpgradePage;
    [SerializeField]
    private UI_TutorialPage uI_TutorialPage;

    private Animator myAnimator;

    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();

        levelNumberText.text = "LEVEL " + GamePlayConfigs.levelNumber;

        if (!PlayerPrefs.HasKey("FirstEnter"))
        {
            PlayerPrefs.SetInt("FirstEnter", 1);
            uI_TutorialPage.gameObject.SetActive(true);
        }

    }


    public void TapPlayButton()
    {
        LoadGameScene();
    }
    public void TapUpgradesButton()
    {
        myAnimator.SetBool("canLoadGameScene", false);
        myAnimator.SetInteger("stateIndex", 1);
    }
    public void TapExitButton()
    {
        Application.Quit();
    }
    public void TapTutorialButton()
    {
        uI_TutorialPage.gameObject.SetActive(true);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("scenes_game");
    }
    public void ShowUpgradePage()
    {
        myAnimator.SetInteger("stateIndex", 3);
        uI_UpgradePage.gameObject.SetActive(true);
    }

}
