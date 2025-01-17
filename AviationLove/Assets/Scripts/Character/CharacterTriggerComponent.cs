using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggerComponent : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private CharacterMovement chMovement;

    [SerializeField]
    private CharacterJUmp jumper;

    [SerializeField]
    private Rigidbody characerBody;

    public GameObject endpanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WallComponent wall) || other.TryGetComponent(out SpikeComponent spike))
        {
            _animator.SetInteger("runningManState", 2);
            chMovement.enabled = false;
            characerBody.isKinematic = false;
            characerBody.constraints = RigidbodyConstraints.None;
            jumper.enabled = false;
            endpanel.SetActive(true);
        }
        else if (other.TryGetComponent(out ScoreGetter cscore))
        {
            Gamemanager.score++;
            Destroy(cscore.gameObject);
        }
        else if (other.TryGetComponent(out MoneyComponent money))
        {
            Gamemanager.moneys += money.moneyGetCount;
            Destroy(money.gameObject);
        }
    }
}
