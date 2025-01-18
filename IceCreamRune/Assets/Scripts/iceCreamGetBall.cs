using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceCreamGetBall : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer SpriteRenderer;

    public int index;

    private void Start()
    {
        index = Random.Range(0, sprites.Length);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = sprites[index];
    }
}
