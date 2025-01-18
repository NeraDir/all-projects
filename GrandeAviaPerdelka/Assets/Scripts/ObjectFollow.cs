using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private void LateUpdate()
    {
        if (_target == null)
            return;

        Vector3 desiredPosition = new Vector3(_target.position.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 2 * Time.deltaTime);

        transform.position = smoothedPosition;

    }
}
