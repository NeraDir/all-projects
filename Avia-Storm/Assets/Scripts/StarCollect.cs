using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Movement>() != null)
        {
            Movement.Instance.CurrerentStars++;
            GlobalSave.StarAmount++;
            Destroy(gameObject);
        }
    }
}
