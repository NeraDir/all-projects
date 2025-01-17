using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuscript : MonoBehaviour
{
    [SerializeField]
    private Text _timeMaxTxt;

    [SerializeField]
    private GameObject _howPlayScreen;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("avidestinyhowToplayscree"))
        {
            _howPlayScreen.SetActive(true);
            PlayerPrefs.SetString("avidestinyhowToplayscree", "showed");
        }
        _timeMaxTxt.text = gamecontrollerscript.maxTimelife.ToString("0.0") + "s";
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("gamescene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
