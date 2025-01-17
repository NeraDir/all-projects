using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private List<Transform> nuzzles;
    [SerializeField]
    private Projectile projectilePrefab;

    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float shootPower;

    private Animator m_Animator;

    private GunManager parent;

    private void OnEnable()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void Init(GunManager parent,Transform nuzzleLookPoint)
    {
        this.parent = parent;

        for (int i = 0; i < nuzzles.Count; i++)
        {
            nuzzles[i].LookAt(nuzzleLookPoint);
        }
    }

    public void StartShooting()
    {
        m_Animator.SetInteger("stateID", 1);
    }

    public void PlayShootAnimationToParent()
    {
        parent.PlayShootAnimation();
    }
    

    public void Shoot()
    {
        for (int i = 0; i < nuzzles.Count; i++)
        {
            Projectile projectileInScene = Instantiate(projectilePrefab, nuzzles[i].position, nuzzles[i].rotation);
            projectileInScene.Init(damage);
            projectileInScene.GetRigidbodyComponent().AddForce(projectileInScene.transform.forward * shootPower, ForceMode.Impulse);
        }


    }

    public void StopShooting()
    {
        m_Animator.SetInteger("stateID", 0);
    }

    public float GetGunAttackSpeed()
    {
        return attackSpeed;
    }
}
