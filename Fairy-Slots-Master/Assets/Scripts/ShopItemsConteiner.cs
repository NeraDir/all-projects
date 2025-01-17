using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemsConteiner : MonoBehaviour
{
    [SerializeField]
    private List<ShopItem> allShopItems;



    private void OnEnable()
    {
        for (int i = 0; i < allShopItems.Count; i++)
        {
            allShopItems[i].Init();
        }
    }




}
