using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallDefenceMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _ballsDefenceHowToPlayScreen;

    [SerializeField]
    private Text _ballsDefenceShowMaxWave;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (!PlayerPrefs.HasKey("BallsDefencePimoHowToPlayShowedKey"))
        {
            _ballsDefenceHowToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("BallsDefencePimoHowToPlayShowedKey", 1);
        }
        _ballsDefenceShowMaxWave.text = "WAVE " + BallDefenceGameController.BallsDefenceMaxLivedWave.ToString("0");
    }

    public void OnPimoPlayPress()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("BallsDefenceGameScene");
    }

    public void OnPimoExitPress()
    {
        Application.Quit();
    }
}
