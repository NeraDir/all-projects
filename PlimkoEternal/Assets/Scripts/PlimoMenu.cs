using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlimoMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayWindow;

    [SerializeField]
    private TMP_Text _bestScoreShow;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlimoHowToPlayOpened"))
        {
            _howToPlayWindow.SetActive(true);
            PlayerPrefs.SetInt("PlimoHowToPlayOpened", 1);
        }
        _bestScoreShow.text = GameManager.PlayerBestScore.ToString();
    }

    public void PlayPressed() 
    {
        SceneManager.LoadScene("PlimoGame");
    }

    public void ExitPressed() 
    {
        Application.Quit(); 
    }
}
