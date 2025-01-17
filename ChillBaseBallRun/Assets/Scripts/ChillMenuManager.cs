using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChillMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _chillBaseAboutScreen;

    [SerializeField]
    private Text _chillBaseMaxDistanceReachedShow;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("ChillBaseAboutShoweKey"))
        {
            _chillBaseAboutScreen.SetActive(true);
            PlayerPrefs.SetInt("ChillBaseAboutShoweKey", 1);
        }
        _chillBaseMaxDistanceReachedShow.text = ChillGameController.ChillBaseMaxDistanceReached.ToString("0");
    }

    public void OnChillPlay()
    {
        SceneManager.LoadScene("ChillBaseGame");
    }

    public void OnChillExit()
    {
        Application.Quit();
    }
}
