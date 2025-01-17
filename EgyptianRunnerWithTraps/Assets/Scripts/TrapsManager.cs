using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _trapsObjects;

    private void Start()
    {
        _trapsObjects[Random.Range(0, _trapsObjects.Length)].SetActive(false);
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0,-1), 90 * Time.deltaTime);
    }
}
