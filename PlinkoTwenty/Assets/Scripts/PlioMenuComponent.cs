using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlioMenuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayScreen;

    [SerializeField]
    private TMP_Text _bestScoreShow;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlioTumbleHowToPlayScreen"))
        {
            _howToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("PlioTumbleHowToPlayScreen", 1);
        }
        _bestScoreShow.text = "x" + PiloGameManager.BestScore.ToString();
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
