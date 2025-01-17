using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private float maxHealth = 10; // Максимальное здоровье персонажа.
    public Text Text;

    public static int upgradeHP = 1;
    public static int upgradeStartHP = 0;

    private void Start()
    {
        Text = gameObject.GetComponentInChildren<Text>();
        maxHealth += upgradeStartHP;
        Text.text = maxHealth.ToString();
    }

    private void OnMouseDown()
    {
        maxHealth = float.Parse(Text.text) + upgradeHP; //увеличение здовья по клику на персонаж
        Text.text = maxHealth.ToString();
    }
    public void HandleTouch()
    {
        maxHealth = float.Parse(Text.text) + upgradeHP; //увеличение здовья по клику на персонаж
        Text.text = maxHealth.ToString();
    }
}
