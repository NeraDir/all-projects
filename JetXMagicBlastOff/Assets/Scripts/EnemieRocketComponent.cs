using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemieRocketComponent : MonoBehaviour
{
    private float enemieHealth;

    [SerializeField]
    private Image enemieHealthBar;

    private void Start()
    {
        enemieHealth = GameComponent.enemieHealth;
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, -1, 0) * 2f * Time.deltaTime;
        UpdateHealthBar();
    }

    private void UpdateHealthBar() 
    {
        enemieHealthBar.fillAmount = Mathf.Lerp(enemieHealthBar.fillAmount, enemieHealth / GameComponent.enemieHealth, 8 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerRocketComponent playerRocket))
        {
            GameComponent.isGameLaunched = true;
            Destroy(gameObject);
        }
        else if (other.TryGetComponent(out BulletComponent bullet))
        {
            enemieHealth -= bullet.bulletDamage;
            Destroy(bullet.gameObject);
            if (enemieHealth <= 0)
            {
                GameComponent.score += Random.Range(1, 5);
                Destroy(gameObject);
            }
        }
    }
}
