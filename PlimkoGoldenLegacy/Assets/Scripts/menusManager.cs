using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menusManager : MonoBehaviour
{
    [SerializeField]
    private GameObject howToPlayPage;

    [SerializeField]
    private TMP_Text showBestScore;

    [SerializeField]
    private ShopManager[] _shoppManagers;

    [SerializeField]
    private TMP_Text _coinsTXT;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("ballsFirstEnterSave"))
        {
            howToPlayPage.SetActive(true);
            PlayerPrefs.SetString("ballsFirstEnterSave", "true");
            foreach (var item in _shoppManagers)
            {
                item.OnShopManagerBuyPressed();
            }
        }
        showBestScore.text = "x" + GameManager.bestScore.ToString("0");
    }

    private void LateUpdate()
    {
        _coinsTXT.text = "balance: " + GameManager.coins.ToString();
    }

    public void Play() 
    {
        SceneManager.LoadScene("games");
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
