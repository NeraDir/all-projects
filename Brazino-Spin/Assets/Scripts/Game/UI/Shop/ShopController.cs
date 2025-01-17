using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public List<ItemShopUI> items = new();

    public static int EquipIndex
    {
        get
        {
            if (!PlayerPrefs.HasKey("EquipIndexSave"))
                return 0;
            else
                return PlayerPrefs.GetInt("EquipIndexSave");
        }
        set
        {
            PlayerPrefs.SetInt("EquipIndexSave", value);
        }
    }

    public void ReverseEquipBTNS(int id)
    {
        foreach (var item in items)
        {
            item.ActivateEquipBtn();
        }

        EquipIndex = id;
        GlobalSave.KnifeIndex = id;
        items[id].DisactivateERquipBtn();
    }
}
