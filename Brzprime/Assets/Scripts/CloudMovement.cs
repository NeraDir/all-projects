using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField]
    private CloudTeleporter _cloudTeleporter;
    [SerializeField]
    private Transform _cloudParent;
    [SerializeField]
    private float _cloudMaxSpeed;
    [SerializeField]
    private float _cloudMinSpeed;

    private RectTransform _rectTransform;
    private Rigidbody2D _cloudRigidbody;
    private float _currentCloudSpeed;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _cloudRigidbody = GetComponent<Rigidbody2D>();

        SpeedRandomer(_cloudMinSpeed, _cloudMaxSpeed);
    }

    void FixedUpdate()
    {
        _cloudRigidbody.MovePosition(transform.position - Vector3.right * _currentCloudSpeed * Time.deltaTime);

        if (Vector2.Distance(_cloudParent.position, new Vector2(transform.position.x, _cloudParent.position.y)) > _rectTransform.sizeDelta.x / 2 + Screen.width / 2 && transform.position.x < _cloudParent.transform.position.x)
        {
            _cloudTeleporter.CloudTeleport(gameObject);
            SpeedRandomer(_cloudMinSpeed, _cloudMaxSpeed);
        }
    }

    public void SpeedRandomer(float minSpeed, float maxSpeed)
    {
        _currentCloudSpeed = Random.Range(minSpeed, maxSpeed);
    }
}
