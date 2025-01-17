using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallsSpawnNewRoadComponent : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;

    public static UnityEvent<Transform> spawnNewRoad = new UnityEvent<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent( out WallDestroyerRollerComponent roller))
        {
            spawnNewRoad?.Invoke(spawnPosition);
            Destroy(gameObject);
        }
    }
}
