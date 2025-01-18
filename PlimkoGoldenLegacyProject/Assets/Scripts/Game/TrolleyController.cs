using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TrolleyController : MonoBehaviour
{
    public Image TrolleyIMG;
    public DoorController doorController;
    public Transform TargetPosition;
    public ColorVariant Color;

    public float TimeBeforeStart = 1f;
    public float MinSpeed = 20f;
    public float MaxSpeed = 50f;

    public float Speed = 0f;

    private Vector3 StartPos = Vector3.zero;
    private bool CanMove = false;

    private GameDataSaves dataSaves;

    private BoxCollider2D boxcoll;

    public bool cantrigger;

    private void Start()
    {
        boxcoll = GetComponent<BoxCollider2D>();
        cantrigger = false;
        TrolleyType trolleyType = GameManager.Instance.trolleyTypes[Random.Range(0, GameManager.Instance.trolleyTypes.Count)];

        TrolleyIMG.sprite = trolleyType.sprite;
        Color = trolleyType.Color;

        StartPos = transform.position;
        Speed = Random.Range(MinSpeed, MaxSpeed);
        StartCoroutine(StartMove());
    }

    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(TimeBeforeStart);
        CanMove = true;
    }

    private void RefreshTrolleyAndPosition()
    {

        cantrigger = false;
        TrolleyType trolleyType = GameManager.Instance.trolleyTypes[Random.Range(0, GameManager.Instance.trolleyTypes.Count)];

        TrolleyIMG.sprite = trolleyType.sprite;
        Color = trolleyType.Color;

        transform.position = StartPos;
        transform.localScale = new Vector3(1, 1, 1);
        Speed = Random.Range(MinSpeed, MaxSpeed);
        CanMove = true;
        boxcoll.enabled = true;
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition.position, Speed * Time.deltaTime);

        if (transform.position == TargetPosition.position)
        {
            CanMove = false;
            CheckDoorOnColor();
        }
    }

    public void StopMovementAndDestroyThis()
    {
        CanMove = false;
        transform.DOScale(Vector3.zero, 2f).SetEase(Ease.OutBounce);
        transform.DOMove(Vector3.down * 0.5f, 2f).OnComplete(() => RefreshTrolleyAndPosition());
        cantrigger = false;

    }

    private void Update()
    {
        if (CanMove)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            if (!doorController.OpenBool)
            {
                boxcoll.enabled = false;
                if (doorController.ColorVar != Color)
                {
                    RefreshTrolleyAndPosition();
                }
                else
                {
                    StopMovementAndDestroyThis();
                    GameManager.Instance.heartsCount--;
                }

            }

        }
    }

    private void CheckDoorOnColor()
    {
        if (doorController.ColorVar == Color)
        {
            RefreshTrolleyAndPosition();
            GameManager.Instance.currentScore += Random.Range(5, 10);
        }
        else
        {
            StopMovementAndDestroyThis();
        }
    }
}
