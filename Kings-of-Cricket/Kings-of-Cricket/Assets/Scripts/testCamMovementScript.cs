using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCamMovementScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] _positions;

    private int _idnexOfPositions;

    private void LateUpdate() 
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _idnexOfPositions++;
            
        }

        if (_idnexOfPositions > _positions.Length - 1)
        {
            _idnexOfPositions = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, _positions[_idnexOfPositions].position,15 * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _positions[_idnexOfPositions].rotation, 1.5f * Time.deltaTime);
    }
}
