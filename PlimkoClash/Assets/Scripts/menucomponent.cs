using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menucomponent : MonoBehaviour
{
    [SerializeField]
    private Text _maxDistanceTxt;

    [SerializeField]
    private Text _ballsCountTxt;

    [SerializeField]
    private GameObject _ballHowToPlaySaveKey;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("ballGameHowToPlaySaveKey"))
        {
            _ballHowToPlaySaveKey.SetActive(true);
            PlayerPrefs.SetInt("ballGameHowToPlaySaveKey",1);
        }
    }

    public void OnClickPlay(int index) 
    {
        gamecontoller.levelIndex = index;
        SceneManager.LoadScene("game");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }

    private void LateUpdate()
    {
        _ballsCountTxt.text = gamecontoller.ballStars.ToString("0") + "C";
        _maxDistanceTxt.text = gamecontoller.maxDistance.ToString("0.0") + "m";
    }
}
