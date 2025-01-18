using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceComponent : MonoBehaviour
{
    private GridComponent _grid;

    private void Start()
    {
        _grid = GetComponent<GridComponent>();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
          RemoveItem();   
        }
    }
    
    public void AddItem(GameObject item)
    {
        _grid.AddItem(item);
    }

    public void RemoveItem()
    {
        _grid.RemoveItem();
    }
}
