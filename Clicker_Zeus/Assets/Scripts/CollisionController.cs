using System;
using UnityEngine;
using UnityEngine.UI;

public class CollisionController : MonoBehaviour
{
    private float difference = 0;

    public static bool NextLevel = false;
    public static int Coin = 0;
    public static int currentCoin = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        SetDamage(other);
    }

    private void SetDamage(Collider2D other)
    {
        Text playerHP = gameObject.GetComponentInChildren<Text>();
        Text enemyHP = other.GetComponentInChildren<Text>();

        if (other.CompareTag("Enemy"))
        {
            difference = float.Parse(playerHP.text) - float.Parse(enemyHP.text);
            if (difference >= 0)
            {
                if (difference == 0)
                    Destroy(gameObject);
                Destroy(other.gameObject);
                playerHP.text = difference.ToString();
                currentCoin += int.Parse(enemyHP.text);
            }
            else
            {
                Destroy(gameObject);
                enemyHP.text = Math.Abs(difference).ToString();
            }
        }
        if (other.CompareTag("EnemyBoss"))
        {
            difference = float.Parse(playerHP.text) - float.Parse(enemyHP.text);
            if (difference >= 0)
            {
                if(difference == 0)
                    Destroy(gameObject);
                Destroy(other.gameObject);
                NextLevel = true;
                currentCoin += int.Parse(enemyHP.text);
                playerHP.text = difference.ToString();
                enemyHP.text = "0";
            }
            else
            {
                Destroy(gameObject);
                enemyHP.text = Math.Abs(difference).ToString();
            }
        }
    }

}
