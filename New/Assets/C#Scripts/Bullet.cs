using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 2000;
    Vector2Int _v2i;
    private Rigidbody2D rigidbody2D = null;
    Enemi enemi;
    private int dmg;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void SetTarget(Vector2Int v2i, int i )
    {
        _v2i = v2i;
        enemi = PositionDisposer.allPositions[v2i].GetComponent<Enemi>();
        dmg = i;

        Vector3 difference = enemi.transform.position - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        
    }
    private void FixedUpdate()
    {
        if (_v2i == null)
            return;

        Vector3 difference = enemi.transform.position - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);

        rigidbody2D.velocity = transform.up * _speed * Time.fixedDeltaTime;

        if (Vector2.Distance(transform.position, enemi.transform.position) <= 0.5f)
        {
            DestroyThis();
        }
    }
    private void DestroyThis()
    {
        enemi.GetDmg(dmg);
        Destroy(gameObject);
    }
}
