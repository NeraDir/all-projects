using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotPageManager : MonoBehaviour
{
    [SerializeField]
    private SuperGameController superGameController;

    [SerializeField]
    private TMP_Text currentMoneyDisplay;

    public static int currentMoneyValue;
    public static int currentGainValue;

    [SerializeField]
    private GameObject superGameButton;

    [SerializeField]
    private GameObject gamePlayUIPage;

    public delegate void UpdateBox();
    public static event UpdateBox CloseSlot;

    private void OnEnable()
    {

        LoopIterController.ClickRotButton += ShowSuperGameButton; 
        currentMoneyValue = GameData.Money;



       

    }

    private void Update()
    {
        currentMoneyDisplay.text = currentMoneyValue.ToString("#");
    }

    private void OnDisable()
    {
        LoopIterController.ClickRotButton -= ShowSuperGameButton;
        GameData.Money = currentMoneyValue;
    }

    public void OpenSuperGame()
    {
        superGameController.gameObject.SetActive(true);
        superGameController.gainValue = currentGainValue;
        superGameButton.SetActive(false);
    }

    public void ClosePage()
    {
        if (CloseSlot != null)
        {
            CloseSlot();
        }

        gamePlayUIPage.SetActive(true);

        gameObject.SetActive(false);

       
    }


    public void ShowSuperGameButton()
    {
        superGameButton.SetActive(true);
    }

  

}
