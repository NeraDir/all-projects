using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDirectionComponent : MonoBehaviour
{
    [SerializeField]
    private Material _good;

    [SerializeField]
    private Material _bad;

    private MeshRenderer _renderer;

    private MeshFilter _filter;

    private objectHooleComponent _objectHoleComponent;

    private bool _isTriggered;

    private MeshCollider _collider;

    private goodMergeComponent _goodMergeComponent;

    private int _collisionCount;

    private List<Collider> colliders = new List<Collider>();

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _objectHoleComponent = FindObjectOfType<objectHooleComponent>();
        _collider = GetComponent<MeshCollider>();
        _filter = GetComponent<MeshFilter>();
    }

    private void LateUpdate()
    {
        if (colliders.Count >= 2)
        {
            _renderer.material = _bad;
        }
        else
        {
            _renderer.material = _good;
        }
        if (_isTriggered)
            return;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 40, transform.localScale.z);
        _collider.sharedMesh = _filter.sharedMesh;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Canner"))
            {
                _isTriggered = true;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y <= 0 ? 0 : transform.localScale.y - 40, transform.localScale.z);
                if (!colliders.Contains(collision))
                {
                    colliders.Add(collision);
                }
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        _isTriggered = false;
        colliders.Clear();
    }
}
