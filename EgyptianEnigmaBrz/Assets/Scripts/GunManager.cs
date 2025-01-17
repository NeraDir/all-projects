using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField]
    private List<Gun> allGuns;
    [SerializeField]
    private Transform aimPoint;
    private Animator m_Animator;

    private Gun activeGun;


    private void OnEnable()
    {
        UI_UpgardeLevelPage.UpgradeGunEvent += SetActualGun;
    }
    private void OnDisable()
    {
        UI_UpgardeLevelPage.UpgradeGunEvent -= SetActualGun;
    }

    public void Init()
    {
        SetActualGun();
        m_Animator = GetComponent<Animator>();
        StartCoroutine(startAttackAfterTime(2.5f));
    }

    private IEnumerator startAttackAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        //m_Animator.SetInteger()
        activeGun.StartShooting();
       
        
    }


    private void SetActualGun()
    {
        for (int i = 0; i < allGuns.Count; i++)
        {
            if (i == GameSceneController.activeGunIndex)
            {
                allGuns[i].gameObject.SetActive(true);
                activeGun = allGuns[i];
            }
            else
                allGuns[i].gameObject.SetActive(false);

        }

        activeGun.Init(this, aimPoint);

    }


    public void StopAttack()
    {
        activeGun.StopShooting();
        m_Animator.SetInteger("stateID", 0);
    }
    public void ContinueShooting()
    {
        StartCoroutine(startAttackAfterTime(2.5f));
    }

    public void PlayShootAnimation()
    {
        //m_Animator.Play("Base Layer.Shoot");
        //m_Animator.SetInteger("stateID", 10);
        m_Animator.SetInteger("stateID", 1);
    }

}
