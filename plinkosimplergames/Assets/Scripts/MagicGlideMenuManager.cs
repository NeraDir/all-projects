using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MagicGlideMenuManager : MonoBehaviour
{
    [SerializeField] private Text _magicGlideLifeTimeText;
    [SerializeField] private Text _magicGlideStarsText;
    [SerializeField] private GameObject _magicGlideHowToPlayScreen;
    [SerializeField] private MagicGlideShopManager _magicGlideShopManager;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MagicGlideHowToPlaySaveKey"))
        {
            _magicGlideShopManager.OnBuy();
            _magicGlideHowToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("MagicGlideHowToPlaySaveKey", 1);
        }
        _magicGlideStarsText.text = "x" + MagicGlideGameManager.MagicGlideStarsCount.ToString("0");
        _magicGlideLifeTimeText.text = MagicGlideGameManager.MagicGlideLifeTimeValue.ToString("0.0") + "s";
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("MagicGlideGameScene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
