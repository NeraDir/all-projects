using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnubisMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _infoWindow;

    [SerializeField]
    private Text _maxScoreTxt;

    [SerializeField]
    private Text _coinsTxt;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("anubis_progress_info_showed"))
        {
            _infoWindow.SetActive(true);
            PlayerPrefs.SetInt("anubis_progress_info_showed", 1);
        }
        _maxScoreTxt.text = AnubisUserData.BestScore.ToString();
    }

    private void LateUpdate()
    {
        _coinsTxt.text = AnubisUserData.Coins.ToString();
    }

    public void OnAnubisPlayButtonPressed()
    {
        SceneManager.LoadScene("AnubisGame");
    }

    public void OnAnubisExitButtonPressed()
    {
        Application.Quit();
    }
}
