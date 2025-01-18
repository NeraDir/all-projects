using UnityEngine;

[ExecuteAlways]
public class CamFollowingScript : MonoBehaviour
{
    [SerializeField]
    private Transform _targetFollower;

    [SerializeField]
    private float _targetingSpeed;

    [SerializeField]
    private Vector3 _targetingFollowingOffset;

    private void LateUpdate()
    {
        if (_targetFollower == null)
            return;
        Vector3 targetingPosition = _targetFollower.position + _targetingFollowingOffset;
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, targetingPosition, _targetingSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
