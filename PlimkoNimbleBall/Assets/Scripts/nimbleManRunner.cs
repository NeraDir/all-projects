using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nimbleManRunner : MonoBehaviour
{
    [SerializeField]
    private Joystick _nmbleJs;

    private Rigidbody _nimbleBody;

    private bool _isRunning = false;

    private void Start()
    {
        _nimbleBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (_isRunning)
        {
            _nimbleBody.velocity = new Vector3(-_nmbleJs.Horizontal * 12, _nimbleBody.velocity.y, _nimbleBody.velocity.z);
            return;
        }
        _nimbleBody.velocity = new Vector3(_nmbleJs.Horizontal * 12, _nimbleBody.velocity.y, 12);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out nimbleBallGet ballGet))
        {
            if (ballGet.isTriggered)
                return;
            ballGet.isTriggered = true;
            nimbleGameManager.nimbleAddNewBallToCase?.Invoke(ballGet);
        }
        if (other.TryGetComponent(out nimbleFinishRoad finish))
        {
            _isRunning = true;
            _nimbleBody.velocity = Vector3.zero;
            nimbleGameManager.nimblePlayerFinished?.Invoke();
        }
        if (other.TryGetComponent(out nimbleTrap trap))
        {
            nimbleGameManager.nimblePlayerDeath?.Invoke();
        }
    }
}
