using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMnagaer : MonoBehaviour
{
    public EntityState currentState;
    public Animator animator;
    public float speed = 20f;
    public float Damage = 30f;
    public float attackSpeed = 1.7f;
    public EnemySpawnController SpawnController;
    [HideInInspector] public Quaternion lastRotation;

    public Joystick MovementJoystick;
    private HealthManager HealthSystem;
    public Rigidbody _rb;
    private bool CanAttack = true;
    [HideInInspector] public bool AttackNow = false;

    private void Start()
    {
        MovementJoystick = GameManager.Instance.MovementJoystick;

        HealthSystem = GetComponent<HealthManager>();
        if (HealthSystem != null)
        {
            HealthSystem.Init();
        }

        UpdateStats();

        _rb = GetComponent<Rigidbody>();

        SetState(new PlayerIdleState());
    }

    private void Update()
    {
        if (currentState != null)
            currentState.StateLogic();
    }

    public void UpdateStats()
    {
        HealthSystem.Health = HealthSystem.MaxHealth + (HealthSystem.MaxHealth * GameManager.HealthMultiplier);
        HealthSystem.MaxHealth = HealthSystem.MaxHealth + (HealthSystem.MaxHealth * GameManager.HealthMultiplier);

        if(HealthSystem.healthText != null)
            HealthSystem.healthText.text = "Health: " + HealthSystem.Health.ToString("0");

    }

    public void SetState(EntityState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(newState.GetState());
        }

        currentState = newState;
        currentState.EnterState(this);
    }

    public void AttackNearEnemy()
    {
        if (CanAttack)
        {
            AttackNow = true;
            EnemyManager nearestEnemy = null;
            animator.SetInteger("State", 2);

            float ama = 9999999999;

            List<EnemyManager> list = new List<EnemyManager>();
            list.AddRange(SpawnController.GetActiveEnemies());

            for (int i = 0; i < list.Count; i++)
            {
                if (ama > Vector3.Distance(transform.position, list[i].transform.position))
                {
                    ama = Vector3.Distance(transform.position, list[i].transform.position);
                    nearestEnemy = list[i];
                }
            }

            if (ama <= 20f)
                nearestEnemy.GetComponent<HealthManager>().minusHP(Damage + Damage * GameManager.DamageMultiplier);

            StartCoroutine(waitBeforeAttack());
        }
    }

    IEnumerator waitBeforeAttack()
    {
        CanAttack = false;
        yield return new WaitForSeconds(attackSpeed - attackSpeed * GameManager.SpeedMultiplier);
        CanAttack = true;
        AttackNow = false;
    }
}
