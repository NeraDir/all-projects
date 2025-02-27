using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCrazTideFruitPlaceComponent : MonoBehaviour
{
    public FruitType fruitType;

    private MagicCrazTidePartComponent _part;

    private void Start()
    {
        _part = GetComponentInParent<MagicCrazTidePartComponent>();
    }

    public void Destruction()
    {
        _part.Destruction(fruitType);
    }
}
