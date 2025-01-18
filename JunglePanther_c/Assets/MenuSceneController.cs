using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject shopPanel;

    [SerializeField]
    private TMP_Text coinsDisplayTXT;
    public static int coinCount;


    private void OnEnable()
    {
        coinCount = PantherRunnerData.coins;
    }

    private void Update()
    {
        coinsDisplayTXT.text = coinCount.ToString();
    }

    public void ClickPlayButton()
    {
        PantherRunnerData.coins = coinCount;
        SceneManager.LoadScene("Panther_game");
    }
    public void ClickShopButton()
    {
        menuPanel.SetActive(false);
        shopPanel.SetActive(true);
    }
    public void ClickExitButton()
    {
        Application.Quit();
    }

    public void ClickCloseButton()
    {
        PantherRunnerData.coins = coinCount;
        menuPanel.SetActive(true);
        shopPanel.SetActive(false);
    }

}
