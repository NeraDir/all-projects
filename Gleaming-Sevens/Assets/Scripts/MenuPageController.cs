using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuPageController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyCountText;

    [SerializeField]
    private GameObject howToPlayPage;

    [SerializeField]
    private GameObject recoverCoinsButton;

    private void OnEnable()
    {
        moneyCountText.text = GameData.Money.ToString();

        if (GameData.Money <= 0)
        {
            recoverCoinsButton.SetActive(true);
        }
        else
        {
            recoverCoinsButton.SetActive(false);
        }
    }


    public void ClickHowToPlayButton()
    {
        howToPlayPage.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ClickPlayButton()
    {
        SceneManager.LoadScene("level_1");
    }
    public void ClickExitButton()
    {
        Application.Quit();
    }

    public void ClickCloseHowToPlayPage()
    {
        howToPlayPage.SetActive(false);
        gameObject.SetActive(true);
    }

    public void ClickRecoverButton()
    {
        GameData.Money = 100;
        moneyCountText.text = GameData.Money.ToString();
        recoverCoinsButton.SetActive(false);
    }
}
