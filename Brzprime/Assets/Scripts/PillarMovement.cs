using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarMovement : MonoBehaviour
{
    [SerializeField]
    private PillarTeleporter _pillarTeleporter;
    [SerializeField]
    private Transform _pillarParent;
    [SerializeField]
    private float _pillarSpeed;

    private Rigidbody2D _pillarRigidbody;
    private RectTransform _rectTransform;
    private GameObject _pillarChild;

    void Awake()
    {
        _pillarRigidbody = GetComponent<Rigidbody2D>();
        _rectTransform = GetComponent<RectTransform>();
        _pillarChild = transform.GetChild(0).gameObject;
    }

   
    void FixedUpdate()
    {
        _pillarRigidbody.MovePosition(transform.position - Vector3.right * _pillarSpeed * Time.deltaTime);

        if(Vector2.Distance(_pillarParent.position, new Vector2(transform.position.x, _pillarParent.position.y)) > _rectTransform.sizeDelta.x / 2 + Screen.width / 2 && transform.position.x < _pillarParent.transform.position.x)
        {
            _pillarTeleporter.PillarTeleport(gameObject, _pillarChild);
        }
    }
}
