using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LostMenuController : MonoBehaviour
{
    public void ClickCloseGame() 
    {
        Application.Quit();
    }

    public GameObject htwp;

    public TMP_Text showEasyAccuracy;

    public TMP_Text showMiddleAccuracy;

    public TMP_Text showHardAccuracy;

    private void LateUpdate()
    {
        showEasyAccuracy.text = LostGamePlayerSaves.lostEasylvlAccuracy.ToString("0") + "%";
        showMiddleAccuracy.text = LostGamePlayerSaves.lostMiddlelvlAccuracy.ToString("0") + "%";
        showHardAccuracy.text = LostGamePlayerSaves.lostHardlvlAccuracy.ToString("0") + "%";
    }

    private void Start()
    {
        Time.timeScale = 1;
        if (!PlayerPrefs.HasKey("htwpLoad"))
        {
            htwp.SetActive(true);
            PlayerPrefs.SetString("htwpLoad", "23423");
        }
    }
}
