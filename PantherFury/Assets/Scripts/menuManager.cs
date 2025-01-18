using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject instructionPage;

    [SerializeField]
    private TMP_Text showScoreBest;


    private void LateUpdate()
    {
        showScoreBest.text = mathManager.bestScore.ToString("0");
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("pantherMathFirstEnter"))
        {
            instructionPage.SetActive(true);
            PlayerPrefs.SetString("pantherMathFirstEnter", "yes");
        }
    }

    public void OnClickPlay() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
