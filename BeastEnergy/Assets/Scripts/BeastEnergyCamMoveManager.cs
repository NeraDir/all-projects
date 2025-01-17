using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BeastEnergyCamMoveManager : MonoBehaviour
{
    [SerializeField] private Vector3 _beastEnergyOffset;

    [SerializeField] private Transform[] _beastEnergyTarget;

    [SerializeField] private float _beastEnergySpeed;

    private Vector3 _beastEnergyBeginOffset = new Vector3(0,11.6f,-14.2f);

    private Vector3 _beastEnergyGameOffset = new Vector3(0, 27.3f, -26.2f);

    private void Start()
    {
        foreach (Transform t in _beastEnergyTarget)
        {
            if (t.parent.gameObject.activeInHierarchy)
            {
                transform.position = new Vector3(t.position.x + _beastEnergyBeginOffset.x, t.position.y + _beastEnergyBeginOffset.y, t.position.z + _beastEnergyBeginOffset.z);
            }
        }
    }

    private void LateUpdate()
    {
        if (!BeastEnergyGameManager.beastEnergyRunLaunched)
            return;
        foreach (Transform t in _beastEnergyTarget)
        {
            if (t.parent.gameObject.activeInHierarchy)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(0, t.position.y + _beastEnergyGameOffset.y, t.position.z + _beastEnergyGameOffset.z), _beastEnergySpeed * Time.deltaTime);
            }
        }
    }
}
