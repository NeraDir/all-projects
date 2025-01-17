using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Crystal : MonoBehaviour, IPointerClickHandler
{
    public CrystalColor color;
    private Direction direction;

    private Transform mTransform;
    private float moveSpeed;

    private bool isdead;

    private Animator animator;

    public void Init(Direction direction, float speed)
    {
        this.direction = direction;
        this.moveSpeed = speed;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        mTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (isdead)
            return;
        mTransform.position += Vector3.right * ((direction == Direction.Right ? 1 : -1) * moveSpeed);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isdead)
            return;
        isdead = true;
        animator.enabled = true;
        if (GameManager.targetColor != color)
        {
            GameManager.scoreCount -= Random.Range(70, 100);
        }
        else
        {
            GameManager.scoreCount += Random.Range(70, 100);
        }
        Destroy(gameObject, 0.5f);

    }
}

public enum CrystalColor
{
    Green,
    Red,
    Orange,
    Blue
}