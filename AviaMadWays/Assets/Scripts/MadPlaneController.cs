using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadPlaneController : MonoBehaviour
{
    public Transform[] positions;

    private SpriteRenderer spriteRederer;

    private float direction;

    private void Start()
    {
        spriteRederer = GetComponent<SpriteRenderer>();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveX(positions[0].position.x, 20, false).OnComplete(()=> spriteRederer.flipX = true));
        sequence.Append(transform.DOMoveX(positions[1].position.x, 20, false).OnComplete(() => { spriteRederer.flipX = false; FindObjectOfType<MadStarsSpawner>().SpawnStars(); }));

        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    public void MoveTo(int direction) 
    {
        this.direction = direction;
    }

    public void MoveUp()
    {
        direction = 0;
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, direction, 0) * 1 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MadStarComponent>())
        {
            collision.GetComponent<MadStarComponent>().DestroyMe();
            MadGameManager.collectedStars += 1;
        }
        if (collision.GetComponent<MadSpikes>())
        {
            MadGameManager.madHeal--;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MadWindComponent>())
        {
            direction = collision.GetComponent<MadWindComponent>().direct;
        }
    }
}
