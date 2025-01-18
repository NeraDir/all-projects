using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bganimation : MonoBehaviour
{
    [SerializeField]
    private Vector3 _direction;

    [SerializeField]
    private float _speed;

    private RectTransform _recter;

    private void Start()
    {
        _recter = GetComponent<RectTransform>();
        _recter.SetLeft(0);
        _recter.SetRight(-3110.41f);
    }

    private void LateUpdate()
    {
        transform.position += _direction * _speed * Time.deltaTime;
        if (_recter.offsetMin.x <= -3110.41f)
        {
            _recter.SetLeft(0);
            _recter.SetRight(-3110.41f);
        }
    }
}
