using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ShopItem", order = 1)]
public class Item : ScriptableObject
{
    public Sprite ProductSprite;
    public int Cost;
    public string saveKey;
}
