using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlatformHealth : MonoBehaviour
{
    public static float maxHealth;
    public static float currentHealth;

    public delegate void PlayerDeathDelegate();
    public static event PlayerDeathDelegate PlayerDeadEvent;

    private void OnEnable()
    {
    }

    public void TakeDamage(float value)
    {
        if (currentHealth - value > 0)
            currentHealth -= value;
        
        else
        {
            if (PlayerDeadEvent != null)
                PlayerDeadEvent();
        }
    }
}
