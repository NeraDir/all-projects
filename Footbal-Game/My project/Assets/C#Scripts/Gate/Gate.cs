using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gate : MonoBehaviour
{
    [SerializeField] private bool _playerGate = false;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Ball>() != null)
            RaundMenager.istance.GameOver(!_playerGate);
    }
}
