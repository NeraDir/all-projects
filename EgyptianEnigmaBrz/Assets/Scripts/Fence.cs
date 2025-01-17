using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField]
    private GunPlatformHealth gunPlatformHealth;



    public GunPlatformHealth GetGunPlatformHealth()
    {
        return gunPlatformHealth;
    }

}
