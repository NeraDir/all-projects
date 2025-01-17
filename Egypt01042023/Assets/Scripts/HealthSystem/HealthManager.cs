using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float MaxHealth = 100f;
    [SerializeField] public float Health = 100f;
    public TMP_Text healthText;

    public IHealth HealtModifier;

    public void Init()
    {
        HealtModifier = GetComponent<IHealth>();

        if(healthText != null )
            healthText.text = "Health: " + Health.ToString("0");
    }

    public void minusHP(float Damage)
    {
        Health -= Damage;

        if(Health <= 0)
        {
            HealtModifier.Die();
        }

        if (healthText != null)
            healthText.text = "Health: " + Health.ToString("0");
    }

    public void SetHP()
    {
        Health = MaxHealth;

        if (healthText != null)
            healthText.text = "Health: " + Health.ToString("0");
    }
}
