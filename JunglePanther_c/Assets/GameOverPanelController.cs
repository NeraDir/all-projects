using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverPanelController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text distanceDisplayTXT;
    [SerializeField]
    private TMP_Text coinsDisplayTXT;



    private void OnEnable()
    {
        distanceDisplayTXT.text = "DISTANCE: " + MovementManager.playerZpos.ToString("#m");
        coinsDisplayTXT.text = "COINS: " + GamePanelManager.gameCoinCount;

        Time.timeScale = 0;
    }



    private void OnDisable()
    {
        PantherRunnerData.coins += GamePanelManager.gameCoinCount;

        Time.timeScale = 1;
    }

    public void ClickMenuButton()
    {
        SceneManager.LoadScene("Panther_menu");
    }

}
