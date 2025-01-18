using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PimoMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayScreen;

    [SerializeField]
    private TMP_Text _maxScore;

    [SerializeField]
    private TMP_Text _maxBalls;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PimoMacheryHowToPlay"))
        {
            _howToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("PimoMacheryHowToPlay", 1);
        }
        _maxBalls.text = "x" + PimoGameController.BallsMaxCount.ToString();
        _maxScore.text = PimoGameController.MaxScore.ToString();
    }

    public void OnClickPaly()
    {
        SceneManager.LoadScene("PimoScene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
