using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_GamePlayPage : MonoBehaviour
{
    [SerializeField]
    private UI_ActionButtonsManager uI_ActionButtonsManager;

    [SerializeField]
    private UI_PausePage uI_PausePage;

    [SerializeField]
    private TMP_Text levelNumberText;
    [SerializeField]
    private TMP_Text coinCountText;

    public delegate void ActionChoiceDelegate(ActionButtonTypes actionTypes);
    public static event ActionChoiceDelegate ActionSelectedEvent;

    private void OnEnable()
    {
        UI_ActionButtonsManager.TapActionButtonEvent += CloseActionButtonsPanel;
    }
    private void OnDisable()
    {
        UI_ActionButtonsManager.TapActionButtonEvent -= CloseActionButtonsPanel;
    }

    private void Start()
    {
        levelNumberText.text = "LEVEL " + GamePlayConfigs.levelNumber;

        if (GamePlayConfigs.coinsCount == 0)
        {
            coinCountText.text = "0";
        }
        else
        {
            coinCountText.text = GamePlayConfigs.coinsCount.ToString("#");

        }


    }


    public void ShowActionButtonsPanel()
    {
        uI_ActionButtonsManager.gameObject.SetActive(true);
    }
    public void CloseActionButtonsPanel(ActionButtonTypes resultActionType)
    {
        uI_ActionButtonsManager.gameObject.SetActive(false);

        if (ActionSelectedEvent != null)
            ActionSelectedEvent(resultActionType);
        
    }
   
    public void TapPauseButton()
    {
        uI_PausePage.gameObject.SetActive(true);
    }
}


