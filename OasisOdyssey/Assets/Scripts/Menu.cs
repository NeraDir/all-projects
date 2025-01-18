using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject oasisHowToPlaingPanel;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TMP_Text displayMaxLevel;

    [SerializeField]
    private TMP_Text displayBestScore;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("oasisPlayerFirstEnterSave"))
        {
            animator.gameObject.SetActive(false);
            oasisHowToPlaingPanel.SetActive(true);
            PlayerPrefs.SetString("oasisPlayerFirstEnterSave", "yes");
        }
    }

    public void Play() 
    {
        animator.SetBool("anim_state", true);
        Invoke("LoadGame", 0.5f);
    }

    public void Exit() 
    {
        animator.SetBool("anim_state", true);
        Invoke("ExitingFromGame", 0.5f);
    }

    private void LoadGame() 
    {
        SceneManager.LoadScene("Gamei");
    }

    private void ExitingFromGame() 
    {
        Application.Quit();
    }

    private void LateUpdate()
    {
        displayBestScore.text = CannonController.maxScore.ToString("0");
        displayMaxLevel.text = "X" + CannonController.maxCombos.ToString("0");
    }


}
