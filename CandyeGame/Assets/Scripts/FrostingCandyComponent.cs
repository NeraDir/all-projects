using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrostingCandyComponent : MonoBehaviour
{
    [SerializeField]
    private Sprite[] candysSprites;

    public Sprite mySprite;

    private void Start()
    {
        mySprite = candysSprites[Random.Range(0, candysSprites.Length)];
        GetComponent<Image>().sprite = mySprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FrostingWallComponent wall))
        {
            if (!wall.isOpen)
            {
                transform.DOKill();
                transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
            }
        }
    }
}
