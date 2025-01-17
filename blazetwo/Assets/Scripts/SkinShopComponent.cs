using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopComponent : MonoBehaviour
{
    [SerializeField]
    private TMP_Text priceTxt;

    [SerializeField]
    private int price;

    [SerializeField]
    private int indexTop;

    [SerializeField]
    private int indexBottom;

    [SerializeField]
    private string skinName;

    [SerializeField]
    private SkinShopComponent[] skinSjops;

    private int Buyed
    {
        get
        {
            if (PlayerPrefs.HasKey(skinName))
            {
                return PlayerPrefs.GetInt(skinName);
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt(skinName, value);
        }
    }

    private void Start()
    {
        Updater();
    }

    private void Updater()
    {
        if (Buyed != 1)
        {
            priceTxt.text = "x" + price.ToString();
        }
        else
        {
            if (GameController.TopSkinIndex == indexTop && GameController.BottomSkinIndex == indexBottom)
            {
                priceTxt.text = "EQUIPED";
            }
            else
            {
                priceTxt.text = "EQUIP";
            }
        }
    }

    public void OnClickBuy()
    {
        if (Buyed != 1)
        {
            if (GameController.MaxScore >= price)
            {
                GameController.MaxScore -= price;
                Buyed = 1;
                OnEquip();
            }
        }
        else
        {
            OnEquip();
        }
    }

    private void OnEquip()
    {
        GameController.TopSkinIndex = indexTop;
        GameController.BottomSkinIndex = indexBottom;
        foreach (var item in skinSjops)
        {
            item.Updater();
        }
    }
}
