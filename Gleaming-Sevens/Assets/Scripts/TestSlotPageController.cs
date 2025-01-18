using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestSlotPageController : MonoBehaviour
{
    [SerializeField]
    private GameObject gamePlayPanel;

    [SerializeField]
    private TMP_Text currentMoneyText;
    [SerializeField]
    private TMP_Text rewardText;
    [SerializeField]
    private TMP_Text spinCountText;

    private int spinCount;
    private int balanceValue;
    private int rewardValue;

    [SerializeField]
    private GameObject spinButton;

    public delegate void SlotComplete();
    public static event SlotComplete SlotGamesComleted;


    private void OnEnable()
    {
        spinCount = LevelController.spinCount;
        balanceValue = GameData.Money;
        rewardValue = 0;
        spinButton.SetActive(true);
    }


    private void Update()
    {
        currentMoneyText.text = balanceValue.ToString();
        spinCountText.text = spinCount.ToString();
        rewardText.text = rewardValue.ToString();
    }

    private void OnDisable()
    {
        GameData.Money = balanceValue;
        LevelController.spinCount = spinCount;
    }


    public void Spin()
    {
        if (spinCount > 0)
        {
            spinCount--;
            rewardValue = Random.Range(-100, 100);
            balanceValue += rewardValue;
        }
        else
        {
            spinButton.SetActive(false);
        }
    }


    public void CloseSlot()
    {
        gamePlayPanel.SetActive(true);
        gameObject.SetActive(false);

        if (SlotGamesComleted != null)
        {
            SlotGamesComleted();
        }
    }

}
