using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text showBestScore;

    [SerializeField]
    private GameObject _instructionPanel;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("FirstEnteredCrazy"))
        {
            _instructionPanel.SetActive(true);
            PlayerPrefs.SetString("FirstEnteredCrazy", "true");
        }
    }

    public void OnPlayButtonPressed() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }

    private void LateUpdate()
    {
        showBestScore.text = GameManager.BestPlayerTime.ToString("0");
    }
}
