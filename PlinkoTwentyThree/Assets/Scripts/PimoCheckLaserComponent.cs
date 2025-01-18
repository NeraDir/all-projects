using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimoCheckLaserComponent : MonoBehaviour
{
    private PimoTargetMove _currentTargetMove;

    public void OnClickCheck(int activeIs)
    {
        if (_currentTargetMove != null)
        {
            if (_currentTargetMove.enablesChecked)
                return;
            _currentTargetMove.enablesChecked = true;
            if (_currentTargetMove.GetEnables() == activeIs)
            {
                _currentTargetMove.OnGoodClick();
            }
            else
            {
                PimoGameController.doSomthingWithHearts?.Invoke(-1);
            }
            _currentTargetMove.DoDestroy();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PimoTargetMove target))
        {
            _currentTargetMove = target;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _currentTargetMove = null;
    }
}
