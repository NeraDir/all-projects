using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_GameController : MonoBehaviour
{

    [SerializeField]
    private TMP_Text spinCountTMP;
    [SerializeField]
    private TMP_Text timeTMP;


    [SerializeField]
    private GameObject _pausePanel;

    public delegate void TapButton();
    public static event TapButton TapRightButton;
    public static event TapButton TapLeftButton;



    private void Update()
    {
        spinCountTMP.text = MainGameManager.currenttSpinCount.ToString("#");
        timeTMP.text = MainGameManager.gameTime.ToString("#s");
    }

    public void TapRightBtn()
    {
        if (TapRightButton != null)
        {
            TapRightButton();
        }
    }
    public void TapLeftBtn()
    {
        if (TapLeftButton != null)
        {
            TapLeftButton();
        }
    }

    public void TapPauseBtn()
    {
        _pausePanel.SetActive(true);
        gameObject.SetActive(false);
    }
}