using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCell : MonoBehaviour
{// плохое название исправить пока не поздно!!!
    public Vector2Int gridLocation;
    public bool isBlocked = false;
    private void OnDestroy()
    {
        Map.instance.map.Remove(gridLocation);
    }
    public void Render(int sortingOrder, Color color)
    {

        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().sortingOrder = sortingOrder + 1;
            GetComponent<SpriteRenderer>().color = color;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == 5) //5 = UI layer
            {
                return true;
            }
        }
        return false;
    }

    private void OnMouseDown()
    {
        if (IsPointerOverUIObject())
            return;
        CharacterControlling.instance.OnCellDown(gridLocation);
    }
    public void OnPathImage()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void OffPathImage()
    {

        GetComponent<SpriteRenderer>().color = Color.clear;
    }
}
