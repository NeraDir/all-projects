using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlay;

    [SerializeField] private GameObject _menuScreen;

    [SerializeField]
    private Text _maxDistance;

    [SerializeField]
    private Text _maxStars;

    [SerializeField]
    private ballBuyContainer _buyContainer;

    private void Start()
    {
        Time.timeScale = 1;
        if (!PlayerPrefs.HasKey("howToPlayPlimkoPolygonsDataSave"))
        {
            _howToPlay.SetActive(true);
            _menuScreen.SetActive(false);
            _buyContainer.Buy();
            PlayerPrefs.SetInt("howToPlayPlimkoPolygonsDataSave", 1);
        }
        _maxDistance.text = gameManager.maxReachedDistance.ToString("0") + "m";
       
    }

    private void LateUpdate()
    {
        _maxStars.text = "x" + gameManager.maxStarsCount.ToString("0");
    }

    public void OnPressPlay() 
    {
        SceneManager.LoadScene("gameScene");
    }

    public void OnPressedExit()
    {
        Application.Quit();
    }
}
