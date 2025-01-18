using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControlling : MonoBehaviour
{
    [SerializeField]
    private string sceneGame;

    [SerializeField]
    private Image howToPlayImage;

    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private TMP_Text bestScoreTxt;

    private void Start()
    {
        if (GameDataSaves.PlayerFirstEnteredInGame.Equals("false"))
        {
            howToPlayImage.gameObject.SetActive(true);
            GameDataSaves.PlayerFirstEnteredInGame = "true";
        }
        playButton.onClick.AddListener(OnPlayButtonPressed);
        exitButton.onClick.AddListener(OnExitButtonPressed);
        bestScoreTxt.text = GameDataSaves.PlayerBestScoreValue.ToString();
    }

    private void OnPlayButtonPressed() 
    {
        SceneManager.LoadScene(sceneGame);
    }

    private void OnExitButtonPressed() 
    {
        Application.Quit();
    }
}
