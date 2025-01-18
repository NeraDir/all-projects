using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Transform m_BulletSpawnPosition;

    [SerializeField]
    private GameObject[] m_BulletsPrefab;

    [SerializeField]
    private GameObject[] m_WeaponsPrefab;

    private AnimationManager m_AnimationManager;

    private void Awake()
    {
        m_AnimationManager = GetComponent<AnimationManager>() ? GetComponent<AnimationManager>() : GetComponentInChildren<AnimationManager>();
        foreach (var item in m_WeaponsPrefab)
        {
            item.SetActive(false);
        }
        m_WeaponsPrefab[PlayerDatas.m_SelectedWeapon].SetActive(true);
    }



    public void Shoot() 
    {
        m_AnimationManager.SetAnimationState(1,"PlayerAnimationState");
    }

    public void SpawnBullet() 
    {
        Instantiate(m_BulletsPrefab[PlayerDatas.m_SelectedWeapon], m_BulletSpawnPosition.position, m_BulletSpawnPosition.rotation);
    }

    public void SetIdleAnimation() 
    {
        m_AnimationManager.SetAnimationState(0, "PlayerAnimationState");
    }
}
