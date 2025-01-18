using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject howToPlayPage;

    [SerializeField]
    private Text bestScoreTXT;

    public static int BestScore 
    {
        get
        {
            if (PlayerPrefs.HasKey("SpaceDefenderSaveKey"))
                return PlayerPrefs.GetInt("SpaceDefenderSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("SpaceDefenderSaveKey", value);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MenuShowedHowToPlaySaveKey"))
        {
            howToPlayPage.SetActive(true);
            PlayerPrefs.SetInt("MenuShowedHowToPlaySaveKey", 1);
        }
        bestScoreTXT.text = BestScore.ToString();
    }

    public void OnClickStartGame() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickEndGame() 
    {
        Application.Quit(); 
    }
}
