using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AllSellebleItems : MonoBehaviour
{
    public List<ShopItem> shopebleItems = new List<ShopItem>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (!PlayerPrefs.HasKey("purpleSaveBuyKey"))
        {
            shopebleItems[0].itemBuyedIdent = 1;
            shopebleItems[0].itemEquipedIdent = 1;
        }
    }

    public ShopItem GetEquipedItem()
    {
        foreach (ShopItem item in shopebleItems)
        {
            if (item.itemEquipedIdent == 1)
            {
                return item;
            }
        }
        return null;
    }
}
