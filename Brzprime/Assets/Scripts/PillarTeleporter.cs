using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarTeleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject _lastPillarObject;

    public void PillarTeleport(GameObject _pillarToTeleport, GameObject _pillarChild)
    {
        _pillarToTeleport.transform.position = _lastPillarObject.transform.position;
        _lastPillarObject = _pillarChild;
    }
}
