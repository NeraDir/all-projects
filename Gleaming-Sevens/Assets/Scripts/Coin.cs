using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    private Transform myTransform;
    private Animator myAnimator;

    public delegate void DetectCoin();
    public static event DetectCoin CoinHasBeenTrigger;

    private bool oneShotTrigger;

    private void OnEnable()
    {
        oneShotTrigger = false;
        myTransform = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        myTransform.Rotate(0, rotateSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (!oneShotTrigger)
            {
                oneShotTrigger = true;
               
                if (CoinHasBeenTrigger != null)
                {
                    CoinHasBeenTrigger();
                    myAnimator.SetBool("isTrigger", true);

                }
                else
                {
                    Debug.Log("GOVNO");

                }
            }
        }

    }

    public void DestroyCoin()
    {
        Destroy(transform.parent.gameObject);
    }
}
