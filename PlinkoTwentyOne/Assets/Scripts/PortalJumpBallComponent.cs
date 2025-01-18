using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalJumpBallComponent : MonoBehaviour
{
    private Rigidbody _ballBody;

    public static UnityEvent<bool> levelCompleted = new UnityEvent<bool>();

    private bool _isEnd;

    private void Start()
    {
        _ballBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (_isEnd)
        {
            _ballBody.velocity = Vector3.zero;
            return;
        }
           
        _ballBody.velocity = new Vector3(_ballBody.velocity.x, _ballBody.velocity.y, PortalSpawnRoadsComponent.currentSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PortalRoadComponent road))
        {
            transform.parent = road.transform;
        }
        if (other.TryGetComponent(out PortalCoinGetComponent coin))
        {
            coin.OnGetUse();
        }
        if (other.TryGetComponent(out PortalJumpFinishComponent finish))
        {
            _isEnd = true;
            levelCompleted?.Invoke(true);
        }
        if (other.TryGetComponent(out PortalJumpDead dead))
        {
            _isEnd = true;
            levelCompleted?.Invoke(false);
        }
    }
}
