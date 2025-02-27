using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CANNONCOMPONENT : MonoBehaviour
{
    [SerializeField]
    private float _distance;

    [SerializeField]
    private BULLETCOMPONENT _bulletPrefab;

    [SerializeField]
    private Transform _spawnPosition;

    private Transform _target;

    public void Awake()
    {
        StartCoroutine(Targeting());
        StartCoroutine(Shooting());
    }

    private IEnumerator Targeting()
    {
        while (true)
        {
            FindNearestRightTarget();
            yield return new WaitForSeconds(0.5f); 
        }
    }

    private void FindNearestRightTarget()
    {
        MOVEMENTCOMPONENT[] allTargets = FindObjectsOfType<MOVEMENTCOMPONENT>();
        Transform bestTarget = null;
        float closestDistance = _distance;

        foreach (MOVEMENTCOMPONENT potentialTarget in allTargets)
        {
            Transform potentialTransform = potentialTarget.transform;
            Vector3 toTarget = potentialTransform.position - transform.position;

            if (Vector3.Dot(transform.right, toTarget.normalized) > 0)
            {
                float distance = toTarget.magnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestTarget = potentialTransform;
                }
            }
        }

        _target = bestTarget;
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            if (_target != null)
            {
                transform.LookAt(_target.position);

                if (Vector3.Distance(transform.position, _target.position) <= _distance)
                {
                    BULLETCOMPONENT newBullet = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
                    newBullet.target = _target;
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
