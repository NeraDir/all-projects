using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool isMoving; // Двигается пока не дошёл до игрока
    Animator animator;
    EnemySpawn enemySpawn;
    public static float EnemyDamage 
    {
        get 
        {
            if (PlayerPrefs.HasKey("PlayerHelatth")) 
            {
                return PlayerPrefs.GetFloat("PlayerHelatth");
            }
            return 0.1f;
        }
        set 
        {
            PlayerPrefs.SetFloat("PlayerHelatth",value);
        }
    }
    public int enemyHealth = 1;

    private GameObject loosePanel;

    void Start()
    {
        enemySpawn = FindObjectOfType<EnemySpawn>();
        loosePanel = GameObject.Find("LoosePanel");
        isMoving = true;
        animator = GetComponent<Animator>();
        StartCoroutine(DamageUpgrade()); // Увеличивается сила удара врага
    }

    
    void Update() // Тут только движение в сторону игрока
    {
        if (isMoving)
        {
            transform.LookAt(enemySpawn.Player.transform.position);
            transform.position = new Vector3
                (transform.position.x,
                transform.position.y, transform.position.z - 8 * Time.deltaTime);
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isMoving = false; // Остановка
            animator.SetBool("isAttack", true);
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage() // Тут было HP Bar ввиде Cube. и он уменьшался у игрока каждый удар
    {
        
        while (true)
        {
            
            yield return new WaitForSeconds(2f);
            enemySpawn.PlayerHP.localScale = new Vector3(enemySpawn.totalScale - EnemyDamage, 1, 1);
            enemySpawn.totalScale -= EnemyDamage;

            if (enemySpawn.totalScale <= 0)
            {
                PlayerController.isEnded = true;
                Time.timeScale = 0; 
            }
        }

    }

    IEnumerator DamageUpgrade() // увеличение силы удара врага
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            EnemyDamage += 0.1f;
        }
    }

    

    
}
