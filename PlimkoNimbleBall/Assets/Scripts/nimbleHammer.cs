using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nimbleHammer : MonoBehaviour
{
    [SerializeField]
    private GameObject _nimbleHamEffect;

    [SerializeField]
    private Transform _spawnPos;


    public void OnShoot()
    {
        Instantiate(_nimbleHamEffect, _spawnPos.position, _nimbleHamEffect.transform.rotation);
    }
}
