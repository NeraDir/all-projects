using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pinoHowToPlayPage;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PinoSorceyHowToPlayfdsgarghasKey"))
        {
            _pinoHowToPlayPage.SetActive(true);
            PlayerPrefs.SetInt("PinoSorceyHowToPlayfdsgarghasKey", 1);
        }
    }

    public void OnClickPlay(int index)
    {
        gameManagerTemper.levelIndex = index;
        SceneManager.LoadScene("SampleGameScene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
