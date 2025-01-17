using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellController : MonoBehaviour, IDropHandler
{
    public DraggableItem draggableItem;
    public int ID = 0;
    public bool Blocked = false;

    public void InitNewItem(DraggableItem item)
    {
        draggableItem = item;
        draggableItem.parrentAfterDrag = transform;
        draggableItem.transform.SetParent(transform);

        if (ID == draggableItem.pieceStruct.ID)
        {
            Blocked = true;
            GameManager.Instance.AddGoodPiece();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if (Blocked || dropped.GetComponent<DraggableItem>().parrentAfterDrag.GetComponent<CellController>().Blocked) return;

        DraggableItem buffdraggableItem = dropped.GetComponent<DraggableItem>();
        CellController oldCell = buffdraggableItem.parrentAfterDrag.GetComponent<CellController>();
        oldCell.InitNewItem(draggableItem);
        InitNewItem(buffdraggableItem);
    }
}
