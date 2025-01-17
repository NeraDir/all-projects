
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<Item> items = new();
    public ShopItemUI ItemPrefab;
    public Transform ContentTR;

    private List<ShopItemUI> itemsUI = new();

    private void Awake()
    {
        foreach(var item in items)
        {
            ShopItemUI buff = Instantiate(ItemPrefab, ContentTR);
            buff.Init(item.saveKey, item.Cost, item.ProductSprite, this, items.IndexOf(item));

            itemsUI.Add(buff);
        }
    }

    public void ReverseAllEquips(int id)
    {
        foreach (var item in itemsUI)
            item.ActivateEquipBtn();

        itemsUI[id].DisactivateEquipBtn();
        GlobalSave.ChoosenRocket = id;
    }
}
