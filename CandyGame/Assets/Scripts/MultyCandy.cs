using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultyCandy : Candy
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Candy"))
        {
            Debug.Log("candy");

            Candy otherCandy = collision.gameObject.GetComponent<Candy>();

            if (!hasMerged && otherCandy != null && !otherCandy.hasMerged)
            {
                hasMerged = true;
                otherCandy.hasMerged = true;

                CandysHit.Invoke(collision.gameObject, gameObject, collision.gameObject.GetComponent<Candy>().Id);
                Rb.constraints = RigidbodyConstraints2D.None;

            }
        }
    }
}
