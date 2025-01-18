using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicMenuComponent : MonoBehaviour
{
    public TMP_Text magicShowMaxReachedScore;

    public GameObject magicAboutPage;

    private void Start()
    {
        magicShowMaxReachedScore.text = MagicGameManager.magcPlayerMaxReachedScore.ToString("0") + "G";
        if (!PlayerPrefs.HasKey("magicPalyerFirstEnterValierSaveerKLErtsd"))
        {
            magicAboutPage.SetActive(true);
            PlayerPrefs.SetInt("magicPalyerFirstEnterValierSaveerKLErtsd", 1);
        }
    }

    public void ClickLoadGame() 
    {
        SceneManager.LoadScene("MagicGameScene");
    }

    public void ClickCloseGame() 
    {
        Application.Quit();
    }
}
