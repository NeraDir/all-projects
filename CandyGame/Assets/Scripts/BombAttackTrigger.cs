using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombAttackTrigger : MonoBehaviour
{
    public Action Explode;

    private Collider2D trigger;

    private void Start()
    {
        trigger = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        List<GameObject> list = new List<GameObject>();
        if (collision.gameObject.CompareTag("Candy"))
        {
            if (!list.Contains(collision.gameObject))
            {
                list.Add(collision.gameObject);
            }
        }
        StartCoroutine(TimerBoom(list));
    }

    private IEnumerator TimerBoom(List<GameObject> list)
    {
        yield return new WaitForSeconds(3);
        foreach (GameObject obj in list)
        {
            Destroy(obj);
        }
        Explode.Invoke();
        Destroy(GetComponentInParent<BombCandy>().gameObject);
    }
}
