using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject[] healther;

    public GameObject panel;

    public int health;

    public void TakeDamage()
    {
        health -= 1;
        for (int i = 0; i < healther.Length; i++)
        {
            if (i < health)
            {
                healther[i].SetActive(true);
            }
            else
            {
                healther[i].SetActive(false);
            }
        }

        if (health <= 0)
        {
            GameControiller.isPlay = true;
            panel.SetActive(true);
        }
    }
}
