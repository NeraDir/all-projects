using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private GameObject BuyBtn;
    [SerializeField] private GameObject EquipBtn;
    [SerializeField] private Image ProductImage;
    [SerializeField] private TMP_Text CostTXT;

    private int cost = 0;
    private string saveKey;
    private int id = 0;
    private ShopManager manager;

    private int buied
    {
        get
        {
            if (!PlayerPrefs.HasKey(saveKey))
                return 0;
            else
                return PlayerPrefs.GetInt(saveKey);
        }
        set
        {
            PlayerPrefs.SetInt(saveKey, value);
        }
    }

    public void Init(string saveKey, int cost, Sprite productSprite, ShopManager manager, int id)
    {
        this.saveKey = saveKey;
        this.cost = cost;
        ProductImage.sprite = productSprite;
        this.manager = manager;
        this.id = id;

        CostTXT.text = $"x{cost}";

        if (buied > 0)
        {
            BuyBtn.SetActive(false);
            //CostTXT.gameObject.SetActive(false);
            if (id == GlobalSave.ChoosenRocket)
            {
                EquipBtn.SetActive(false);
            }
            else
            {
                EquipBtn.SetActive(true);
            }
        }
        else
        {
            CostTXT.gameObject.SetActive(true);
            BuyBtn.SetActive(true);
            EquipBtn.SetActive(false);
        }
    }

    public void Buy()
    {
        if (GlobalSave.StarAmount >= cost)
        {
            buied++;
            GlobalSave.StarAmount -= cost;
            //CostTXT.gameObject.SetActive(false);
            Equip();
        }
    }

    public void ActivateEquipBtn()
    {
        if (buied > 0)
        {
            EquipBtn.SetActive(true);
        }
    }

    public void DisactivateEquipBtn()
    {
        if (buied > 0)
        {
            EquipBtn.SetActive(false);
        }
    }

    public void Equipss()
    {
        manager.ReverseAllEquips(id);
    }

    public void Equip()
    {
        BuyBtn.SetActive(false);
        Equipss();
    }
}
