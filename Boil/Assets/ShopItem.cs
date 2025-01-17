using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int index;
    public string itemKey;
    public int price;

    public Sprite sprite;

    public Button buyButton;
    public Button equipButton;
    public TMP_Text priceTextDisplay;


    private ItemState currentState;
    private List<ItemState> allStates = new() { ItemState.NotBought, ItemState.NotEquipted, ItemState.Equipted };

    //[HideInInspector]
    public int stateIndex;


    public delegate void EquipItemDelegate(int index);
    public static event EquipItemDelegate ItemEquptedEvent;



    private void OnEnable()
    {
        priceTextDisplay.text = price.ToString();

       // buyButton.onClick.AddListener(BuyItem);
       // equipButton.onClick.AddListener(EquipItem);
    }

    private void Start()
    {
      //  buyButton.onClick.AddListener(BuyItem);
        equipButton.onClick.AddListener(EquipItem);

        GetComponent<Image>().sprite = sprite;

    }




    public void LoadItemData()
    {
        stateIndex = 0;

        if (!PlayerPrefs.HasKey(itemKey))
        {
            if(index == Configs.ballSkinIndex)
            {
                stateIndex = 2;
            }
            else
            {
                stateIndex = 0;
            }

            PlayerPrefs.SetInt(itemKey, stateIndex);
        }


        stateIndex = PlayerPrefs.GetInt(itemKey);


        currentState = allStates[stateIndex];

        PerformState();


    }

    public void BuyItem()
    {

        if (Configs.allCoinsCount - price < 0)
            return;
      
        Configs.allCoinsCount -= price;
        ChangeState(1);

    }

    public void EquipItem()
    {
        ChangeState(2);

        if (ItemEquptedEvent != null)
            ItemEquptedEvent(index);
    }


    private void PerformState()
    {
        if(currentState == ItemState.NotBought)
        {
            buyButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(false);
            priceTextDisplay.transform.parent.gameObject.SetActive(true);
        }
        else if(currentState == ItemState.NotEquipted)
        {
            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);
            priceTextDisplay.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
            priceTextDisplay.transform.parent.gameObject.SetActive(false);
        }
    }

    public void ChangeState(int newStateIndex)
    {
        stateIndex = newStateIndex;
        PlayerPrefs.SetInt(itemKey, stateIndex);
        currentState = allStates[stateIndex];

        PerformState();
    }
}

public enum ItemState
{
    NotBought,
    NotEquipted,
    Equipted
}
