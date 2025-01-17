using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;

    [SerializeField] private EdgeCollider2D collider;

    private readonly List<Vector2> _points = new List<Vector2>();

    private void Start()
    {
        collider.transform.position -= transform.position;
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanApp(pos)) return;
        
        _points.Add(pos);
        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount-1, pos);
        collider.points = _points.ToArray();
    }

    private bool CanApp(Vector2 pos)
    {
        if (_renderer.positionCount == 0) return true;

        return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.Resolution;
    }
}
