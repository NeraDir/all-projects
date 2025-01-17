using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _blaztBlazersHowToPlay;

    [SerializeField]
    private TMP_Text _blaztBlazersMaxLevelTxt;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BlaztBlazterHowToPlaySguisudfgudfKey"))
        {
            _blaztBlazersHowToPlay.SetActive(true);
            PlayerPrefs.SetInt("BlaztBlazterHowToPlaySguisudfgudfKey", 1);
        }
        _blaztBlazersMaxLevelTxt.text = GameManager.MaxLevel.ToString();
    }

    public void onClickPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void onClickQuit()
    {
        Application.Quit(); 
    }
}
