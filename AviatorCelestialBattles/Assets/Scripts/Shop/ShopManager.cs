using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static int SkinNum
    {
        get
        {
            if (!PlayerPrefs.HasKey("SkinNumSave"))
                return -1;

            return PlayerPrefs.GetInt("SkinNumSave");
        }
        set
        {
            PlayerPrefs.SetInt("SkinNumSave", value);
        }
    }

    public List<ShopItem> items = new List<ShopItem>();

    public void EquipAlomost(ShopItem item)
    {
        if (SkinNum == -1)
        {
            SkinNum = item.ID;
            return;
        }
        else
        {
            items[SkinNum - 1].UnEquip();
            SkinNum = item.ID;
        }
    }
}
