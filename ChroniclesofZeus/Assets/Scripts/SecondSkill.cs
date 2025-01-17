using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSkill : Skill
{
    [SerializeField]
    private FirstSkill firstSkillPrefab;

    [SerializeField]
    private int lightningCount;

    [SerializeField]
    private float lifeTime;

    public float Damage;

    public override void Apply(Enemy target)
    {
        /*lightningCount += (GamePlayConfigs.seconddSkillLevel - 1);
        for (int i = 0; i < lightningCount; i++)
        {
            Instantiate(firstSkillPrefab, transform.position, transform.rotation);
        }*/
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
            Destroy(gameObject,2);
        }
    }
}
