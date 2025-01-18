using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPanel;

    [SerializeField]
    private GameObject gamePlayUI;

    [SerializeField]
    private GameObject levelCompletedPage;

    public static int spinCount;


    public int openBoxCount;
    public int maxBoxCount;


    private void OnEnable()
    {
        spinCount = 3;
        openBoxCount = 0;

        Box.BoxHasBeenTrigger += ShowSlot;
        Coin.CoinHasBeenTrigger += AddSpinCount;
        //TestSlotPageController.SlotGamesComleted += SlotCompleted;
        SlotPageManager.CloseSlot += SlotCompleted;
    }
    private void OnDisable()
    {
        Box.BoxHasBeenTrigger -= ShowSlot;
        Coin.CoinHasBeenTrigger -= AddSpinCount;
        SlotPageManager.CloseSlot -= SlotCompleted;
    }

    public void SlotCompleted()
    {
        openBoxCount++;

        if (maxBoxCount == openBoxCount)
        {
            levelCompletedPage.SetActive(true);
            gamePlayUI.SetActive(false);
        }
    }

    public void ShowSlot()
    {
        gamePlayUI.SetActive(false);
        slotPanel.SetActive(true);
    }

    public void AddSpinCount()
    {
        spinCount++;
    }
}
