using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _trampolineFruitTypes;

    private void Start()
    {
        _trampolineFruitTypes[Random.Range(0, _trampolineFruitTypes.Length)].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer ==3)
        {
            gameObject.SetActive(false);
        }
    }
}
