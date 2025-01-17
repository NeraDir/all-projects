using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public Image img;
    public int ID = -1;

    public void INIT(Sprite sprite, int ID)
    {
        transform.DOMoveY(CelestialGameManager.Instance.SpawnPoses[1].Pos.position.y + 50f, 3f).OnComplete(() => transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject)));

        img.sprite = sprite;
        this.ID = ID;
    }

    public void DestroyME()
    {
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
    }
}
