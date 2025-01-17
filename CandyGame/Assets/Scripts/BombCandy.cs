using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCandy : Candy
{
    [SerializeField] private BombAttackTrigger trigger;
    public Boom prefabBoom;

    private void Awake()
    {
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        trigger.gameObject.SetActive(true);
        trigger.Explode += SpawnBoom;
    }

    private void OnMouseDown()
    {
        falled = true;

        Destroy(gameObject);
    }

    private void SpawnBoom()
    {
       Boom boom = Instantiate(prefabBoom);
        boom.transform.position = transform.position;
    }
}
