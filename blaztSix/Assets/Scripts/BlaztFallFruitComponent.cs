using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlaztFallFruitComponent : MonoBehaviour
{
    private Animator animator;

    public bool isClicked;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, -1, 0) * BlaztGameManager.MoveSpeed * Time.deltaTime;
    }

    public void OnClickUse()
    {
        if (isClicked)
            return;
        isClicked = true;
        DestroyMe();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BlaztDeathLine line))
        {
            if (!animator.enabled)
            {
                BlaztGameManager.starsCount -= 1;
                DestroyMe();
            }
        }
    }

    public void DestroyMe()
    {
        animator.enabled = true;
        Destroy(gameObject, 0.5f);
    }
}
