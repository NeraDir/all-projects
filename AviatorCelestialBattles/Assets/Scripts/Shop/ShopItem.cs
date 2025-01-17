using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public ShopManager shopManager;

    public GameObject BuyBtn;
    public GameObject EquipBtn;

    public int Price = 200;
    public int ID = 0;

    public int buided
    {
        get
        {
            if (!PlayerPrefs.HasKey("Buided" + ID))
                return 0;

            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("Buided" + ID, value);
        }
    }

    private void Start()
    {
        if (buided == 0)
        {
            BuyBtn.SetActive(true);
            EquipBtn.SetActive(false);
        }
        else
        {
            BuyBtn.SetActive(false);

            if (ShopManager.SkinNum == ID)
            {
                EquipBtn.SetActive(false);
            }
            else
            {
                EquipBtn.SetActive(true);
            }
        }
    }

    public void Buy()
    {
        if (ValuteController.Instance.MoneySave >= Price)
        {
            buided = 1;
            ValuteController.Instance.AddMoney(-Price);
            BuyBtn.SetActive(false);
            EquiptThisOne();
        }
    }

    public void UnEquip()
    {
        EquipBtn.SetActive(true);
    }

    public void EquiptThisOne()
    {
        EquipBtn.SetActive(false);
        shopManager.EquipAlomost(this);
    }
}
