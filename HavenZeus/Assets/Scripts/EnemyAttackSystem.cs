using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour
{
    [SerializeField]
    private float _enemyDamage;

    private HeroHealthSystem _heroHealthSystem;

    private void Awake()
    {
        _heroHealthSystem = GameObject.Find("MainHero").GetComponent<HeroHealthSystem>();
    }

    public void GiveDamage()
    {
        _heroHealthSystem.ApplyDamage(_enemyDamage);
    }
}
