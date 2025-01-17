using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private float _price;

    private int levelNumber;
    private int featureValue;
    [SerializeField]
    private string featureNameString;

    [SerializeField]
    private TMP_Text priceTMP;

    public TMP_Text _levelTMP;
    public TMP_Text _featureTMP;

    [SerializeField]
    private int maxLevel;

    [SerializeField]
    private GameObject _butButton;

    public void Init()
    {


        //UI_DisplayMoney.money = BoatGameData.allCoinsCount;

        if (itemType == ItemType.Boat)
        {
            featureNameString = "speed";
            levelNumber = BoatGameData.boatSpeedLevelNumber;
        }
        else if (itemType == ItemType.Bet)
        {
            featureNameString = "coins";
            levelNumber = BoatGameData.betValueLevelNumber;
        }
        else if (itemType == ItemType.Time)
        {
            featureNameString = "seconds";
            levelNumber = BoatGameData.gameTimeLevelNumber;
        }

        CalculateParametersForItem();

    }

    public void Update()
    {
        _levelTMP.text = "LEVEL" + levelNumber.ToString("#");
        _featureTMP.text = featureValue + featureNameString;
        priceTMP.text = _price.ToString("#");
    }

    public void TapBuyBtn()
    {
        if (UI_DisplayMoney.money - _price >= 0)
        {
            UI_DisplayMoney.UpdateMoney((int)-_price);

            levelNumber++;

           

            CalculateParametersForItem();

        }
    }

    public void CalculateParametersForItem()
    {


        if (levelNumber == maxLevel)
        {
            _butButton.SetActive(false);
        }
        else
        {
            if (itemType == ItemType.Boat)
            {
                BoatGameData.boatSpeedLevelNumber = levelNumber;
                featureValue = 10 * levelNumber;
            }
            else if (itemType == ItemType.Bet)
            {
                BoatGameData.betValueLevelNumber = levelNumber;
                featureValue = 100 + ((levelNumber - 1) * 15);
            }
            else if (itemType == ItemType.Time)
            {
                BoatGameData.gameTimeLevelNumber = levelNumber;
                featureValue = 15 + ((levelNumber - 1) * 5);
            }
        }



        
    }

}

public enum ItemType
{
    Boat,
    Bet,
    Time
}

