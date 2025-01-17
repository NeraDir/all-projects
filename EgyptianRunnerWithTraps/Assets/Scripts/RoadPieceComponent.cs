using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPieceComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject[] traps;

    [SerializeField]
    private Transform[] trapsPositions;

    private void Start()
    {
        foreach (var item in trapsPositions)
        {
            Instantiate(traps[Random.Range(0, traps.Length)], item.position, Quaternion.identity);
        }
    }
}
