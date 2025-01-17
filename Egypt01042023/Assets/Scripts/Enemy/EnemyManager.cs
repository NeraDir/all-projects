using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float StopDistance = 3f;
    public EnemySpawnController SpawnController;

    private Animator animator;
    private Transform PlayerTransform;
    public HealthManager HealthSystem;
    private bool isInitialized = false;

    private float AttackTime = 2f;
    private float _timer = 0;

    public HealthManager playerHealtManager;

    public void Init(Transform playerTR, EnemySpawnController contr)
    {
        HealthSystem = GetComponent<HealthManager>();
        if (HealthSystem != null)
        {
            HealthSystem.Init();
        }

        animator = GetComponent<Animator>();
        PlayerTransform = playerTR;
        SpawnController = contr;
        playerHealtManager = PlayerTransform.GetComponent<HealthManager>();
        isInitialized = true;
    }

    private void Update()
    {
        if(isInitialized)
        {
            if(Vector3.Distance(transform.position, PlayerTransform.position) > StopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.position, 10f * Time.deltaTime);
                transform.LookAt(new Vector3(playerHealtManager.transform.position.x, transform.position.y, playerHealtManager.transform.position.z));
                animator.SetInteger("State", 2);
            }
            else
            {
                animator.SetInteger("State", 1);
                _timer += Time.deltaTime;

                if(_timer >= AttackTime)
                {
                    playerHealtManager.minusHP(0.5f);
                }

                //transform.LookAt(new Vector3(playerHealtManager.transform.position.x, transform.position.y, playerHealtManager.transform.position.z));
            }
        }
    }
}
