using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ShopData",menuName = "Create ShopData")]
public class ShopData : ScriptableObject
{
    [System.Serializable]
    public struct ShopDat
    {
        public int price;
        public int index;
        public Sprite bgSprite;
    }
    public ShopDat[] shopDats;
}