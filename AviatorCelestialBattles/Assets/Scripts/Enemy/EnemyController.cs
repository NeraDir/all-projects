using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Image IMG;
    public List<EnemySpriteStruct> EnemySprites = new();
    public int ID = -1;

    public int SpriteID = -1;

    public void INIT(int ID)
    {
        this.ID = ID;

        int rnd = Random.Range(0, EnemySprites[this.ID].sprites.Count);

        IMG.sprite = EnemySprites[this.ID].sprites[rnd].sprite;
        SpriteID = EnemySprites[this.ID].sprites[rnd].SpriteID;

        transform.DOMoveY(-1000, 15f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            var bullet = collision.GetComponent<Bullet>();

            if(bullet.ID == SpriteID)
            {
                ValuteController.Instance.AddMoney(50);
                ValuteController.Instance.AddScore(120);

                Destroy(gameObject);
                bullet.DestroyME();
            }
        }
    }
}

[System.Serializable]
public struct EnemySpriteStruct
{
    public List<SpriteStruct> sprites;
}

[System.Serializable]
public struct SpriteStruct
{
    public Sprite sprite;
    public int SpriteID;
}