using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ResultWindow : Window
{
    [SerializeField] private Text showResult;

    [SerializeField] private GameObject[] stars;

    private void Start()
    {
        showResult.text = "LEVEL " + (GameController.CurrentLevel + 1).ToString();
        Invoke(nameof(ShowStars), 0.5f);
    }

    public override void Init()
    {
        
        base.Init();
    }

    private void ShowStars()
    {
        foreach (var item in stars)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void OnNext()
    {
        PlayerPrefs.SetInt($"SloZenCurrentStarsCount{GameController.CurrentLevel}SaveKey", 3);
        PlayerPrefs.SetString($"Level{GameController.CurrentLevel}SloZenCompletedSaveKey", true.ToString());
        ShopWindow.Coins += 10;
        GameController.CurrentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenu()
    {
        ShopWindow.Coins += 10;
        PlayerPrefs.SetInt($"SloZenCurrentStarsCount{GameController.CurrentLevel}SaveKey", 3);
        PlayerPrefs.SetString($"Level{GameController.CurrentLevel}SloZenCompletedSaveKey", true.ToString());
        SceneManager.LoadScene("Menu");
    }
}
