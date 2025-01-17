using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlatform : MonoBehaviour
{
    private GunManager gunManager;

    private void OnEnable()
    {
       
    }

    public void Init()
    {
        gunManager = GetComponent<GunManager>();
        gunManager.Init();
    }
    public void StopAttackWithGun()
    {
        gunManager.StopAttack();
    }

}
