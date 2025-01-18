using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSegment : MonoBehaviour
{
    [SerializeField]
    private List<Transform> borderPoints;


    public void Init()
    {

    }

    public float GetLenght()
    {
        return Mathf.Abs(borderPoints[1].position.z - borderPoints[0].position.z);
    }
}
