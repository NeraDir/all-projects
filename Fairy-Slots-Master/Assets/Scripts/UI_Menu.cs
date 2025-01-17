using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject shopPanel;



    public void TapPlayBtn()
    {
        SceneManager.LoadScene("game");
    }

    public void TapShopBtn()
    {
        shopPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void TapExitBtn()
    {
        Application.Quit();
    }
}
