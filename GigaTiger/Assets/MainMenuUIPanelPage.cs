using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuUIPanelPage : MonoBehaviour
{
    public TMP_Text recorStarsDiplayText;
    public GameObject howtoplaypanelpage;
    public GameObject openPageLoader;



    private void OnEnable()
    {
        if(GamePlayData.howtoplaydata == "false")
        {
            GamePlayData.howtoplaydata = "true";
            Invoke(nameof(HowToPlay), 3.33f);
        }

        if (GamePlayData.recordstartdata == 0)
        {
            recorStarsDiplayText.text = "";
        }
        else
        {
            recorStarsDiplayText.text = GamePlayData.recordstartdata.ToString();

        }
    }


    public void Run()
    {
        openPageLoader.SetActive(true);
    }
    public void HowToPlay()
    {
        howtoplaypanelpage.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void OpenMenu()
    {
        howtoplaypanelpage.SetActive(false);
    }
}
