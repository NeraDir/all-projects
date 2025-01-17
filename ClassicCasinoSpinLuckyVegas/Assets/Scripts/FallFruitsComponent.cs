using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallFruitsComponent : MonoBehaviour
{
    public int indexOfFruit;

    public Sprite[] fruitSprites;

    private Image _fallFruitImage;

    private void Start()
    {
        _fallFruitImage = GetComponent<Image>();
    }

    public void Init() 
    {
        indexOfFruit = Random.Range(0, fruitSprites.Length);
        _fallFruitImage.sprite = fruitSprites[indexOfFruit];
    }
}
