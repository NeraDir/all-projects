using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemyDieModificator : MonoBehaviour, IHealth
{
    public void Die()
    {
        GetComponent<EnemyManager>().SpawnController.DeleteFromList(GetComponent<EnemyManager>());
        GameManager.Instance.ExpirianceSave += 10;
    }
}
