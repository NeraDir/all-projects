using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _howTOPlayScreen;

    [SerializeField]
    private TMP_Text _showBestLivingTime;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("avia_skies_runners_how_to_play"))
        {
            _howTOPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("avia_skies_runners_how_to_play", 1);
        }
        _showBestLivingTime.text = AviaPlaneController.maxLivingTime.ToString("0.0") + "s";
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
