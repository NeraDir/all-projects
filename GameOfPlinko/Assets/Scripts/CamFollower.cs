using UnityEngine;

[ExecuteAlways]
public class CamFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private float _floatSpeed;

    private void LateUpdate()
    {
        if (_target != null)
            transform.position = Vector3.Lerp(transform.position,_target.position + _offset, _floatSpeed * Time.fixedDeltaTime);
    }
}
