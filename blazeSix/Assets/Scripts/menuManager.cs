using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _infoScreen;

    [SerializeField]
    private TMP_Text _displayBestLivingTime;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BlaztPerfectionInfoSawKey"))
        {
            _infoScreen.SetActive(true);
            PlayerPrefs.SetInt("BlaztPerfectionInfoSawKey", 1);
        }
        _displayBestLivingTime.text = gameController.BestBlaztLivingTimeValue.ToString("0.0");
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
