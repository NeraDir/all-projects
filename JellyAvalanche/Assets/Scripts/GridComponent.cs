using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GridComponent : MonoBehaviour
{
    [SerializeField] private Transform[] _gridPlaces;
    
    [SerializeField]
    private List<GameObject> _gridItems =  new List<GameObject>();

    private void Awake()
    {
        UpdateGrid();
    }
    
    public void AddItem(GameObject item)
    {
        Rigidbody body = item.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.isKinematic = true;
        }
        _gridItems.Add(item);
        UpdateGrid(); 
    }

    public void RemoveItem()
    {
        if (_gridItems.Count > 0)
        {
            GameObject itemToRemove = _gridItems[_gridItems.Count - 1];
            _gridItems.RemoveAt(_gridItems.Count - 1);
            UpdateGrid();
        }
    }
    
    private void UpdateGrid()
    {
        if (_gridItems.Count == 0)
        {
            /*transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
            {
                Destroy(gameObject);
            });*/
        }
        else
        {
            for (int i = 0; i < _gridItems.Count; i++)
            {
                Rigidbody body = _gridItems[i].GetComponent<Rigidbody>();
                if (body != null)
                {
                    Destroy(body);
                }

                _gridItems[i].transform.parent = transform;
                _gridItems[i].transform.DOMove(_gridPlaces[i].position,0.5f);
                _gridItems[i].transform.DORotateQuaternion(_gridPlaces[i].rotation, 0.5f);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (var item in _gridPlaces)
        {
            Gizmos.DrawCube(item.position,new Vector3(0.25f,0.25f,0.25f));
        }
    }
}
