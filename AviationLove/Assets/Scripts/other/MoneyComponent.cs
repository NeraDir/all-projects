using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyComponent : MonoBehaviour
{
    public int moneyGetCount;

    private void Start()
    {
        moneyGetCount = Random.Range(1, 4);
    }
}
