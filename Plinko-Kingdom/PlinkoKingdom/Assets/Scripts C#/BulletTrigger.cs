using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemieHealth enemie)) 
        {
            enemie.TakeDamage(Damage);
            Destroy(gameObject);
        }
        if (other.TryGetComponent(out DestroyerComponent destroyer))
        {
            Destroy(gameObject);
        }
    }
}
