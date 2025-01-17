using UnityEngine;
using System;

[Serializable]
public class CandyItem 
{
    public int index;
    public Sprite sprite;


    public Sprite GetSprite()
    {
        return sprite;
    }

}
