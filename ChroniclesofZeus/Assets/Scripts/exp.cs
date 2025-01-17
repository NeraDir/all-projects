using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mouser"))
        {
            Skillsuse.expCount += 1;
            Destroy(gameObject);
        }
    }
}
