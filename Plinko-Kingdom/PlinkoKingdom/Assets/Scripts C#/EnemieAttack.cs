using UnityEngine;

public class EnemieAttack : MonoBehaviour
{
    public float Damage;

    private Animator animator;

    private bool canAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canAttack = true;
    }

    public void SetDefault() 
    {
        animator.SetInteger("enemie",0);
        canAttack = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth player))
        {
            if (!canAttack)
                return;
            player.TakeDamage(Damage);
            animator.SetInteger("enemie", 1);
            canAttack = false;
        }
    }
}
