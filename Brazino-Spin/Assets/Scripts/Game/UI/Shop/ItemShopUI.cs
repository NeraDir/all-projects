using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemShopUI : MonoBehaviour
{
    [SerializeField] private GameObject BuyBtn;
    [SerializeField] private GameObject EquipBtn;
    public ShopController shopController;
    public TMP_Text CostTXT;
    public TMP_Text GlobalCoinsTXT;
    public string saveKey;
    public int cost;
    public int id;

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

    private void Start()
    {
        GlobalCoinsTXT.text = $"{GlobalSave.CoinsCount}";

        if (buied != 0)
        {
            CostTXT.gameObject.SetActive(false);
            BuyBtn.gameObject.SetActive(false);

            if (id == ShopController.EquipIndex)
            {
                EquipBtn.gameObject.SetActive(false);
            }
        }
        else
        {
            CostTXT.gameObject.SetActive(true);
            CostTXT.text = $"{cost}";

            BuyBtn.gameObject.SetActive(true);
            EquipBtn.gameObject.SetActive(false);
        }
    }

    public void Buy()
    {
        if (cost <= GlobalSave.CoinsCount)
        {
            buied++;
            BuyBtn.gameObject.SetActive(false);
            GlobalSave.CoinsCount -= cost;
            GlobalCoinsTXT.text = $"{GlobalSave.CoinsCount}";
            Equip();
        }
    }

    public void DisactivateERquipBtn()
    {
        EquipBtn.gameObject.SetActive(false);
    }

    public void ActivateEquipBtn()
    {
        if (buied != 0)
            EquipBtn.gameObject.SetActive(true);
    }

    public void Equip()
    {
        shopController.ReverseEquipBTNS(id);
    }
}
