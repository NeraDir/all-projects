using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseOpen : MonoBehaviour
{
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject HTPPanel;

    public int GameSccene = 2;

    public void PlayGame()
    {
        SceneManager.LoadScene(GameSccene);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenShop()
    {
        ShopPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void CloseShop()
    {
        ShopPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void OpenHTP()
    {
        HTPPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void CloseHTP()
    {
        HTPPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
}
