using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Shop Data", menuName = "Create new Shop Data")]
public class ChaseDataOfShop : ScriptableObject
{
   public List<ShopData> shopData = new List<ShopData>();
}

[Serializable]
public class ShopData
{
    public Sprite sprite;
    public int price;
}
