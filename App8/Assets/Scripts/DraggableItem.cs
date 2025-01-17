using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Transform parrentAfterDrag;
    public Image IMG;

    public PieceStructure pieceStruct;

    public void InitCell(PieceStructure _pieceStruct)
    {
        parrentAfterDrag = transform.parent;
        pieceStruct = _pieceStruct;
        IMG.sprite = pieceStruct.sprite;

        if (transform.parent.GetComponent<CellController>().ID == pieceStruct.ID)
        {
            transform.parent.GetComponent<CellController>().Blocked = true;
            GameManager.Instance.AddGoodPiece();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parrentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        IMG.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parrentAfterDrag);
        IMG.raycastTarget = true;
    }
}
