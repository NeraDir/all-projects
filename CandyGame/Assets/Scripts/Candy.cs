using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public static Action<GameObject, GameObject, int> CandysHit;
    public int Id;
    public bool falled = false;

    public Rigidbody2D Rb;
    public bool hasMerged = false;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Candy"))
        {

            if (collision.gameObject.GetComponent<Candy>().Id == Id)
            {
                Candy otherCandy = collision.gameObject.GetComponent<Candy>();

                if (!hasMerged && otherCandy != null && !otherCandy.hasMerged)
                {
                    hasMerged = true;
                    otherCandy.hasMerged = true;

                    CandysHit.Invoke(gameObject, collision.gameObject, Id);
                    Rb.constraints = RigidbodyConstraints2D.None;

                }
            }
        }
    }


}
