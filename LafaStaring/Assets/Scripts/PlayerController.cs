using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject LightGun, MediumGun, HardGun; 
    public static PlayerController instance;
    Animator animator;
    bool isLight, isMedium, isHard; // Включается при нажатии на опред.кнопку
    public float PlayerDamage; // СилаАтакиИгрока
    bool isCanAttack = true; // Что бы нельзя было флудить кнопкой ударов. Пока не true, нельзя нажать

    private GameObject currentSword;

    public GameObject loosePanel;

    public static bool isEnded;

    public static int lightDamage 
    {
        get 
        {
            if (PlayerPrefs.HasKey("LightingSaveKey"))
            {
                return PlayerPrefs.GetInt("LightingSaveKey");
            }
            return 1;
        }
        set 
        {
            PlayerPrefs.SetInt("LightingSaveKey", value);
        }
    }

    

    public static int MediumDamage
    {
        get
        {
            if (PlayerPrefs.HasKey("MediumSaveKey"))
            {
                return PlayerPrefs.GetInt("MediumSaveKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("MediumSaveKey", value);
        }
    }

    public static int HardDamage
    {
        get
        {
            if (PlayerPrefs.HasKey("HardSaveKey"))
            {
                return PlayerPrefs.GetInt("HardSaveKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("HardSaveKey", value);
        }
    }

    private void Awake()
    {
        Time.timeScale = 1;
        isEnded = false;
        instance = this;
        GunsOff(); // Откл всех оружий
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(EnemyHPUpgrade());
    }

    public void LightAttack(GameObject Gun)
    {
        if (isCanAttack)
        {
            isCanAttack = false;
            GunsOff();
            Gun.SetActive(true);
            currentSword = Gun;
            StartCoroutine(AttackAnim());
            isLight = true;
        }
    }

    public void Block() 
    {
        if (isCanAttack)
        {
            isCanAttack = false;
            GunsOff();
            currentSword.SetActive(true);
            StartCoroutine(BlockAnim());
        }
    }

    public void MediumAttack(GameObject Gun)
    {
        if (isCanAttack)
        {
            isCanAttack = false;
            GunsOff();
            Gun.SetActive(true);
            currentSword = Gun;
            StartCoroutine(AttackAnim());
            isMedium = true;
        }
        
    }
    public void HardAttack(GameObject Gun)
    {
        if (isCanAttack)
        {
            isCanAttack = false;
            GunsOff();
            Gun.SetActive(true);
            currentSword = Gun;
            StartCoroutine(AttackAnim());
            isHard = true;
        }
        
    }

    

    IEnumerator AttackAnim()
    {
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.4f);
        Boolean(); 
        yield return new WaitForSeconds(1);
        animator.SetBool("isAttack", false);
        isCanAttack = true;
    }

    IEnumerator BlockAnim()
    {
        animator.SetBool("isBlock", true);
        yield return new WaitForSeconds(0.4f);
        Boolean();
        yield return new WaitForSeconds(1);
        animator.SetBool("isBlock", false);
        isCanAttack = true;
    }

    private void GunsOff()
    {
        LightGun.SetActive(false);
        MediumGun.SetActive(false);
        HardGun.SetActive(false);
    }

    private void Boolean()
    {
        isHard = false;
        isMedium = false;
        isLight= false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Light" && isLight) // если враг с тэгом Light и нажата Light
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.enemyHealth--;
                if (enemy.enemyHealth <= 0)
                {
                    MenuHelper.countDestroyed++;
                    MenuHelper.coinsers += Random.Range(1, 5);
                    Destroy(other.gameObject);
                }
            }
        }

        if (other.tag == "Medium" && isMedium)
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.enemyHealth--;
                if (enemy.enemyHealth <= 0)
                {
                    MenuHelper.countDestroyed++;
                    MenuHelper.coinsers += Random.Range(1, 5);
                    Destroy(other.gameObject);
                }
            }
        }

        if (other.tag == "Hard" && isHard)
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.enemyHealth--;
                if (enemy.enemyHealth <= 0)
                {
                    MenuHelper.countDestroyed++;
                    MenuHelper.coinsers += Random.Range(1, 5);
                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (isEnded) 
        {
            loosePanel.SetActive(true);
        }
    }

    IEnumerator EnemyHPUpgrade() 
        // Я сделал не увелечение ХП врага каждые 15сек, а уменьшение силы удара персонажа (одного и тоже :D) можно переделать
    {
        while (true)
        {
            if (PlayerDamage > 0.1f)
            {
                PlayerDamage -= 0.05f;
                
            }
            yield return new WaitForSeconds(15f);
        }
    }
}
