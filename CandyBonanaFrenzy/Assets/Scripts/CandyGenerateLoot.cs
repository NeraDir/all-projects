using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyGenerateLoot : MonoBehaviour
{
    [SerializeField]
    private CandyLoot[] loots;

    private void OnEnable()
    {
        foreach (var item in loots) 
        {
            item.Init();
        }
    }
}
