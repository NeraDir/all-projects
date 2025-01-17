using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CandysMenumingComponent : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject candysintroductionPanel;

    private bool doingSomthing;

    [SerializeField]
    private TMP_Text candysMaxLevelReachedDisplayer;

    [SerializeField]
    private TMP_Text candysBestScoreDisplayer;

    [SerializeField]
    private Button candysPlayButton;

    [SerializeField]
    private Button CandysExitButton;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CandysIntroductionPassedSaveKey"))
        {
            candysintroductionPanel.SetActive(true);
            animator.gameObject.SetActive(false);
            PlayerPrefs.SetInt("CandysIntroductionPassedSaveKey", 1);
        }
        CandysExitButton.onClick.AddListener(Exit);
        candysPlayButton.onClick.AddListener(Play);
    }

    private void LateUpdate()
    {
        candysBestScoreDisplayer.text = CandysGameManager.candysBestScore.ToString("0");
        candysMaxLevelReachedDisplayer.text = CandysGameManager.candysMaximumAchievedLevel.ToString("0");
    }

    private void Play() 
    {
        animator.SetInteger("CANDY_UI_ANIMATION_STATE", 1);
        doingSomthing = true;
        Invoke(nameof(DoSomthing), 1);
    }

    private void Exit() 
    {
        animator.SetInteger("CANDY_UI_ANIMATION_STATE", 1);
        doingSomthing = false;
        Invoke(nameof(DoSomthing), 1);
    }

    private void DoSomthing() 
    {
        if (doingSomthing)
        {
            SceneManager.LoadScene("GamingScene");
        }
        else
        {
            Application.Quit();
        }
    }
}
