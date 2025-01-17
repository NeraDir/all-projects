using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private MainMenager mainMenager;
    private void Start()
    {
        mainMenager = MainMenager.instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Car>())
        {
            mainMenager.Stop();
        }
    }
}
