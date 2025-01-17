using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuCompo : MonoBehaviour
{
    [SerializeField]
    private GameObject _bonzaGameInfo;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BonzaGameInfoDisplayKey"))
        {
            _bonzaGameInfo.SetActive(true);
            PlayerPrefs.SetInt("BonzaGameInfoDisplayKey", 1);
        }
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Game 1");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
