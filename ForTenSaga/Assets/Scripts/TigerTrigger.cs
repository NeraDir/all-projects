using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip _coinClip;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WallTrigger wall))
        {
            WallSpawnerComponent.playerReachedWall?.Invoke();
            Destroy(wall.gameObject);
        }

        if (other.TryGetComponent(out FinishComponent finish))
        {
            GameManager.resultShow?.Invoke(false);
        }

        if (other.TryGetComponent(out CoinsComponent coinsComponent))
        {
            coinsComponent.Use();
            SettingsManager.playSound?.Invoke(_coinClip);
        }
    }

    private void OnDestroy()
    {
        GameManager.resultShow?.Invoke(true);
    }
}
