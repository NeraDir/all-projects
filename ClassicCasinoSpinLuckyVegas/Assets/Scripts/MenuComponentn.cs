using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuComponentn : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayScreen;

    [SerializeField]
    private Text _balanceShow;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("HasInfoAboutGame"))
        {
            _howToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("HasInfoAboutGame", 1);
        }
        _balanceShow.text = "x" + GameController.currentCoins.ToString();
    }

    public void LoadGame() 
    {
        SceneManager.LoadScene("Game");
    }

    public void CloseGame() 
    {
        Application.Quit();
    }
}
