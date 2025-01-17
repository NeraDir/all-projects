using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuComponent : MonoBehaviour
{
    [SerializeField]
    private SkinShopComponent skinShopComponent;

    [SerializeField]
    private TMP_Text bestLevelTxt;

    [SerializeField]
    private TMP_Text[] scoreTxt;

    [SerializeField]
    private GameObject howToPlaypage;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BlaztOasisHowToPlayKey"))
        {
            howToPlaypage.SetActive(true);
            skinShopComponent.OnClickBuy();
            PlayerPrefs.SetInt("BlaztOasisHowToPlayKey", 1);
        }
        bestLevelTxt.text = GameController.MaxLevel.ToString();
    }

    private void LateUpdate()
    {
        foreach (var item in scoreTxt)
        {
            item.text = "x" + GameController.MaxScore.ToString();
        }
    }

    public void OnClickPaly()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
