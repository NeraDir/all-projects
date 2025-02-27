using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Datas", menuName = "Create Shop Data")]
public class ShopDatas : ScriptableObject
{
    public List<ShopData> shopDatas = new List<ShopData>();
}

[Serializable]
public struct ShopData
{
    public int index;
    public int price;
    public Sprite sprite;
}