using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandieMenuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlayScreen;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CandiesFestivalHowToPlay"))
        {
            _howToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("CandiesFestivalHowToPlay", 1);
        }
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
