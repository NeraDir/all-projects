using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private int maxHealth = 20; // Максимальное здоровье персонажа.
    private Text textHP;

    private void Start()
    {
        textHP = gameObject.GetComponentInChildren<Text>();
        textHP.text = maxHealth.ToString();
    }
}
