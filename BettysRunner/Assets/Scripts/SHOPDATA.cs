using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Data", menuName = "Create Shop Data")]
public class SHOPDATA : ScriptableObject
{
    public ShopItem[] shopItems;
}

[Serializable]
public struct ShopItem
{
    public int id;
    public int price;
    public Sprite sprite;
}