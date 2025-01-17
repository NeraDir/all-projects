using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CamFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _followTarget;

    [SerializeField]
    private float _followSpeed;

    [SerializeField]
    private Vector3 _followOffeset;

    private bool _isFollowing;

    private void Start()
    {
        Application.targetFrameRate = 30;
        objectHooleComponent.lastReached.AddListener(OnLastReached);
    }

    private void OnDestroy()
    {
        objectHooleComponent.lastReached.RemoveListener(OnLastReached);
    }

    private void OnLastReached()
    {
        _isFollowing = true;
    }

    private void LateUpdate()
    {
        if (_isFollowing)
            return;
        transform.position = Vector3.Lerp(transform.position, _followTarget.position + _followOffeset, _followSpeed * Time.deltaTime);
    }
}
