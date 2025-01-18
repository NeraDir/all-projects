using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerBody;

    public float speed;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = new Vector3(mousePos.x - transform.position.x, transform.position.y,0);

        playerBody.velocity = direction * speed;

        if (transform.position.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
