using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menucomponent : MonoBehaviour
{
    [SerializeField]
    private Text _bestDistanceReached;

    [SerializeField]
    private Text _currentCoins;

    [SerializeField]
    private GameObject _howToPlayScreen;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("gameHowToPlayShowedsave"))
        {
            _howToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("gameHowToPlayShowedsave",1);
        }
        _bestDistanceReached.text = gamemanager.maxdistancereachedvalue.ToString("0.0") + "m";
        _currentCoins.text = gamemanager.gamestarsconscount.ToString("0") + "A";
    }

    public void OnPlayPressed() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnExitPressed() 
    {
        Application.Quit();
    }
}
