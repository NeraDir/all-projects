using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private float cooldown;

    private void Start()
    {
        cooldown = 3;
    }

    private void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Candy"))
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                TaskManager.Lost.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cooldown = 3;
    }
}
