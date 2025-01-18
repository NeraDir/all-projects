using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayScreen;

    [SerializeField]
    private TMP_Text _starsCountDisplay;

    [SerializeField]
    private TMP_Text _distancevalueDisplay;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PiloOdysseyHowToPlayScreenDispalyed"))
        {
            _howToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("PiloOdysseyHowToPlayScreenDispalyed", 1);
        }
        _distancevalueDisplay.text = GameManager.BestReachDistance.ToString("0.0") + "m";
        _starsCountDisplay.text = "x" + GameManager.BestEarnStarsCount.ToString();
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
