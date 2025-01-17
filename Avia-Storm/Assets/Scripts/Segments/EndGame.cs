using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Movement>() != null)
        {
            GlobalSave.StarAmount += Movement.Instance.CurrerentStars;
            GlobalSave.RecordMeteres = Movement.Instance.CurrentMetres;
            MapGenerator.Instance.EndGameAFF();

            Destroy(other.gameObject);
        }
    }
}
