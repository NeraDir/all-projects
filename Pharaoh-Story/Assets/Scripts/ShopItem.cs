using System;
using UnityEngine;

[Serializable]
public class ShopItem
{
    public Sprite itemIcon;
    public int itemPrice;
    public GameObject itemModal;
    public string itemSaveKeyBuyed;
    public string itemSaveKeyEquiped;

    public int itemBuyedIdent 
    {
        get 
        {
            if (PlayerPrefs.HasKey(itemSaveKeyBuyed))
            {
                return PlayerPrefs.GetInt(itemSaveKeyBuyed);
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt(itemSaveKeyBuyed, value);
        }
    }

    public int itemEquipedIdent 
    {
        get
        {
            if (PlayerPrefs.HasKey(itemSaveKeyEquiped))
            {
                return PlayerPrefs.GetInt(itemSaveKeyEquiped);
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt(itemSaveKeyEquiped, value);
        }
    }
}
