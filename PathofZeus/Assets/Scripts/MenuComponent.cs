using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuComponent : MonoBehaviour
{
    public TMP_Text showRecord;

    public GameObject howToPlayPanel;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("ZevsGameHowToPlay"))
        {
            howToPlayPanel.SetActive(true);
            PlayerPrefs.SetInt("ZevsGameHowToPlay", 1);
        }

    }

    private void LateUpdate()
    {
        showRecord.text = zevsSaves.LivingTimeRecord.ToString("00.0") +"s";
    }

    public void ONClickPaly() 
    {
        SceneManager.LoadScene("Game");
    }


    public void OnCLickExit() 
    {
        Application.Quit();
    }
}
