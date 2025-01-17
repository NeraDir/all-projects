using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelDisplay;

    [SerializeField]
    private float moveSpeed;

    private int level;
    [SerializeField]
    private float health;

    private Transform m_Transform;
    private Rigidbody m_Rigidbody;
    private Animator m_Animator;

    private Transform target;

    private Coroutine moveCoroutine;
    private Collision lastCollision = null;

    private bool canMove = true;

    private GunPlatformHealth gunPlatformHealth;

    public delegate void ZombieDeathDelegate();
    public static event ZombieDeathDelegate ZombieDeadEvent;



    private void OnEnable()
    {

    }

    public void Init(int level, Transform target)
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();

        this.level = level;
        health = level;
        levelDisplay.text = "lvl. " + level;

        PlayWalkAnimation();
        m_Transform.LookAt(target);
        moveCoroutine = StartCoroutine(move());
    }

    private IEnumerator move()
    {
        while (true)
        {
            if (canMove)
            {
                if(m_Rigidbody != null)
                    m_Rigidbody.velocity = transform.forward * moveSpeed * Time.deltaTime;
            }
            else
            {
                if (m_Rigidbody != null)
                    m_Rigidbody.velocity = Vector3.zero;
            }


            m_Transform.LookAt(target);

            yield return null;
        }

    }

    public void TakeDamage(float value)
    {
        if (health - value > 0)
        {
            health -= value;
            canMove = false;
            PlayTakeDamageAnimation();
        }
        else
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        GameSceneController.scoreCount++;

        levelDisplay.gameObject.SetActive(false);
        StopCoroutine(moveCoroutine);
   
        PlayDeadAnimation();

        if (ZombieDeadEvent != null)
            ZombieDeadEvent();

        Destroy(m_Rigidbody);
        Destroy(GetComponent<CapsuleCollider>());
    }


    public void ContinueMove()
    {
        canMove = true;
    }
    public void DestoyZombie()
    {

        Destroy(gameObject);
    }
    public void Attack()
    {
        if (gunPlatformHealth != null)
            gunPlatformHealth.TakeDamage(1);
    }

    public void StartMove()
    {
        moveCoroutine = StartCoroutine(move());
        PlayWalkAnimation();
    }

    public void FreezeAnimation()
    {
        canMove = false;
        m_Animator.speed = 0;
    }
    public void ContinuePlayAnimation()
    {
        canMove = true;
        m_Animator.speed = 1;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Fence fence))
        {
            if(lastCollision == null)
            {
                lastCollision = collision;
                gunPlatformHealth = fence.GetGunPlatformHealth();
                StopCoroutine(moveCoroutine);
                PlayAttackAnimation();
            }
        }
    }


    public void PlayWalkAnimation()
    {
        m_Animator.SetInteger("stateID", 1);
    }
    public void PlayAttackAnimation()
    {
        m_Animator.SetInteger("stateID", 2);
    }
    public void PlayTakeDamageAnimation()
    {
        m_Animator.SetInteger("stateID", 3);
    }
    public void PlayDeadAnimation()
    {
        m_Animator.SetInteger("stateID", 4);
    }
}
