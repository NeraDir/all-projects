using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragonLanMenu : MonoBehaviour
{
    [SerializeField]
    private Text showCoins;

    [SerializeField]
    private Text showMaxLevel;

    [SerializeField]
    private Text showSoulsCount;

    [SerializeField]
    private GameObject gameInfoPage;

    [SerializeField]
    private DragonLanSkinsManager skinsManager;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("DragonLanGameInfoPageShowedSaveKey"))
        {
            gameInfoPage.SetActive(true);
            skinsManager.Buy();
            skinsManager.OnClickOpenSkin();
            PlayerPrefs.SetInt("DragonLanGameInfoPageShowedSaveKey",1);
        }
    }

    private void LateUpdate()
    {
        showCoins.text = DragonLanGameController.coins.ToString("0");
        showMaxLevel.text = DragonLanGameController.MaxLevel.ToString("0");
        showSoulsCount.text = DragonLanController.DragonLanSoulsCount.ToString("0");
    }

    public void OnClickPlay() 
    {
        SceneManager.LoadScene("DragonLanSampleScene");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
