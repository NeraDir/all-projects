using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IceCreammenumanager : MonoBehaviour
{
    [SerializeField]
    private GameObject _iceCreamRusherAboutScreen;

    [SerializeField]
    private Text _iceCreamRusherMaxReachedLevel;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("IceCreamRusherCaramelAboutScreenShowDataSave"))
        {
            _iceCreamRusherAboutScreen.SetActive(true);
            PlayerPrefs.SetInt("IceCreamRusherCaramelAboutScreenShowDataSave", 1);
        }
        _iceCreamRusherMaxReachedLevel.text = IceCreamGameManager.iceCreamMaxReachedLevel.ToString();
    }

    public void OnIceRusherPlay()
    {
        SceneManager.LoadScene("RusherGame");
    }

    public void OnIceRusherExit()
    {
        Application.Quit();
    }
}
