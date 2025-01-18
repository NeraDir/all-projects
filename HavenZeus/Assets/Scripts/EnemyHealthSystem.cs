using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField]
    private Transform _bonusSpawnPosition;
    [SerializeField]
    private GameObject _healthBonus;
    [SerializeField]
    private GameObject _moneyBonus;
    [SerializeField] [Range(0,100)]
    private float _healthBonusChance;
    [SerializeField] [Range(0, 100)]
    private float _moneyBonusChance;
    [SerializeField]
    private float _maxHealth;

    private LevelCompleter _levelCompleter;

    private void Awake()
    {
        _levelCompleter = GameObject.Find("LevelCompleter").GetComponent<LevelCompleter>();
    }

    public void ApplyDamage(float _health)
    {
        _maxHealth -= _health;

        if (_maxHealth <= 0)
        {
            int moneyBonusChance = Random.Range(0, 100);

            if(moneyBonusChance < _moneyBonusChance)
            {
                Instantiate(_moneyBonus, transform.position + Vector3.up * 3, Quaternion.identity);
            }

            int healthBonusChance = Random.Range(0, 100);

            if (healthBonusChance < _healthBonusChance)
            {
                Instantiate(_healthBonus, transform.position + Vector3.up * 3, Quaternion.identity);
            }
            _levelCompleter.CheckLevelWin();
            Destroy(gameObject);
        }
    }
}
