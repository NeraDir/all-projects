using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesManagerController : MonoBehaviour
{
    public static SpritesManagerController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public Sprite[] CEllSprites;
    public Sprite defaultSprite;
}
