using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiddayFoodComponent : MonoBehaviour,IDragHandler, IEndDragHandler
{
    private bool middayFoodCanMove;

    public Canvas canvas;

    public int index;

    private void Start()
    {
        middayFoodCanMove = false;
    }

    private void LateUpdate()
    {
        if (middayFoodCanMove)
            return;
        transform.position += new Vector3(1, 0, 0) * (50 * MiddayGameManager.middayCurrentLevel) * Time.deltaTime;
    }

    public void OnDrag(PointerEventData eventData)
    {
        PointerEventData data = eventData;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, data.position, canvas.worldCamera, out position);
        middayFoodCanMove = true;
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MiddayFoodLineEnd"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("MiddayTigerFoodEatPleace"))
        {
            if (MiddayGameManager.middayNeedFoodIndex == index)
            {
                MiddayGameManager.middayTotalFood -= 1;
                transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
            }
            else
            {
                MiddayGameManager.middayPlayerHeartsCount -= 1;
                transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
            }
        }
    }

}
