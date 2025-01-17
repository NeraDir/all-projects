using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject hp;
    public Image actHp;
    public void UpdateHpBar(float health, float actialHealth)
    {
        actHp.fillAmount = actialHealth / health;
    }
}
