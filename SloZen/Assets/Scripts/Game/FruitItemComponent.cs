using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    Green,
    Blue,
    Orange,
    Yellow,
    Red,
    White,
    Purple,
    Brown,
    BlackGreen
}

public class FruitItemComponent : MonoBehaviour
{
    public FruitType fruitType;

    private void OnDestroy()
    {
        GameController.curreFruitsList.Remove(this);
        GameController.destroyedFruits += 1;
    }
}
