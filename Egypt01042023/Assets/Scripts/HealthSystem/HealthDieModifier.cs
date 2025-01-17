using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDieModifier : MonoBehaviour, IHealth
{
    public void Die()
    {
        Debug.Log("PlayerDie");
    }
}
