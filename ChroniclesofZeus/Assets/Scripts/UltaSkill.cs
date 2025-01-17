using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltaSkill : Skill
{
    [SerializeField]
    private float lifeTime;

    public float Damage;

    private void Start()
    {
        StartCoroutine(lifeTimer());
    }

    public override void Apply(Enemy target)
    {
       
    }

    private IEnumerator lifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
            Destroy(gameObject, 0);
        }
    }
}
