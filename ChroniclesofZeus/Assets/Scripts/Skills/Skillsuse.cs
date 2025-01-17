using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Skillsuse : MonoBehaviour
{
    [SerializeField]
    private FirstSkill lgStrike;

    [SerializeField]
    private SecondSkill secondSkill;


    [SerializeField]
    private thirdSkill ThirdSkill;


    [SerializeField]
    private UltaSkill ultaSkill;

    public static float expCount;

    public float firstSkillCd;
    public float secondSkillCd;
    public float thirdSkillCd;
    public float fourthSkillCd;


    public Image firstSkillCoulDownImage;
    public Image secondSkillCoulDownImage;
    public Image thirdSkillCoulDownImage;
    public Image fourthSkillCoulDownImage;


    public TMP_Text firstSkillCoulDownTXT;
    public TMP_Text secondSkillCoulDownTXT;
    public TMP_Text thirdSkillCoulDownTXT;
    public TMP_Text fourthSkillCoulDownTXT;

    private float timer1 = 0;
    private float timer2 = 0;
    private float timer3 = 0;
    private float timer4 = 0;

    public float fDamage;
    public float sDamage;
    public float tDamage;
    public float foDamage;

    [SerializeField]
    private GameObject button1;

    [SerializeField]
    private GameObject button2;

    [SerializeField]
    private GameObject button3;

    [SerializeField]
    private GameObject button4;

    private bool skillOneOpen;
    private bool skillTwoOpen;
    private bool skillThreeOpen;
    private bool skillFourOpen;

    [SerializeField]
    private TMP_Text showMyCurrentLevel;

    private int currentLevel;

    private int maxExp;

    private int currentClickUpgrades;

    public static int maxRechedLevel 
    {
        get 
        {
            if (PlayerPrefs.HasKey("MaxRechedLevel"))
            {
                return PlayerPrefs.GetInt("MaxRechedLevel");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("MaxRechedLevel", value);
        }
    }


    public static int firstSkillCastCount
    {
        get
        {
            if (PlayerPrefs.HasKey("firstSkillCastCountSaveKey"))
            {
                return PlayerPrefs.GetInt("firstSkillCastCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("firstSkillCastCountSaveKey", value);
        }
    }

    public static int secondSkillcastCount
    {
        get
        {
            if (PlayerPrefs.HasKey("secondSkillcastCountSaveKey"))
            {
                return PlayerPrefs.GetInt("secondSkillcastCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("secondSkillcastCountSaveKey", value);
        }
    }

    private void Start()
    {
        timer1 = firstSkillCd;
        timer2 = secondSkillCd;
        timer3 = thirdSkillCd;
        timer4 = fourthSkillCd;
        skillOneOpen = true;
        currentLevel = 1;
        currentClickUpgrades = 0;
        maxExp = 10;
    }

    private void LateUpdate()
    {
        if (currentLevel == 5 && !skillTwoOpen)
        {
            button2.SetActive(true);
        }
        if (currentLevel == 10 && !skillThreeOpen)
        {
            button3.SetActive(true);
        }
        if (currentLevel == 15 && !skillFourOpen)
        {
            button4.SetActive(true);
        }

        if (expCount >= maxExp)
        {
            currentClickUpgrades++;
            expCount = 0;
            currentLevel++;
            maxExp += 5;
            if (skillOneOpen)
                button1.SetActive(true);
            if(skillTwoOpen)
                button2.SetActive(true);
            if(skillThreeOpen)
                button3.SetActive(true);
            if(skillFourOpen)
                button4.SetActive(true);
        }


        maxRechedLevel = currentLevel;

        showMyCurrentLevel.text = "LVL " + currentLevel.ToString("0");


        if (skillOneOpen) 
        {
            timer1 -= Time.deltaTime;
            if (timer1 <= 0)
            {
                timer1 = firstSkillCd;
                FirstSkill fs = Instantiate(lgStrike, transform.position, Quaternion.identity);
                fs.damage = fDamage;
                firstSkillCoulDownImage.fillAmount = 1;
            }
            firstSkillCoulDownTXT.text = timer1.ToString("0.0");
            UpdateFillers(firstSkillCoulDownImage, firstSkillCd, timer1);
        }

        if (skillTwoOpen)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                timer2 = secondSkillCd;
                Enemy[] enemies = FindObjectsOfType<Enemy>();
                foreach (var item in enemies)
                {
                    if ((Vector3.Distance(transform.position, item.transform.position)) <= 5)
                    {
                        SecondSkill sc = Instantiate(secondSkill, new Vector3(item.transform.position.x, item.transform.position.y + 1, item.transform.position.z), Quaternion.identity);
                        sc.Damage = sDamage;
                        break;
                    }
                }
                secondSkillCoulDownImage.fillAmount = 1;
            }
            secondSkillCoulDownTXT.text = timer2.ToString("0.0");
            UpdateFillers(secondSkillCoulDownImage, secondSkillCd, timer2);
        }


        if (skillThreeOpen)
        {
            timer3 -= Time.deltaTime;
            if (timer3 <= 0)
            {
                timer3 = thirdSkillCd;
                thirdSkill thirdSkiller = Instantiate(ThirdSkill, transform.position, Quaternion.identity);
                thirdSkiller.damage = tDamage;
                thirdSkillCoulDownImage.fillAmount = 1;
            }
            thirdSkillCoulDownTXT.text = timer3.ToString("0.0");
            UpdateFillers(thirdSkillCoulDownImage, thirdSkillCd, timer3);
        }

        if (skillFourOpen)
        {
            timer4 -= Time.deltaTime;
            if (timer4 <= 0)
            {
                timer4 = fourthSkillCd;
                Enemy[] enemies = FindObjectsOfType<Enemy>();
                foreach (var item in enemies)
                {
                    UltaSkill sc = Instantiate(ultaSkill, new Vector3(item.transform.position.x, item.transform.position.y + 1, item.transform.position.z), ultaSkill.transform.rotation);
                    sc.Damage = sDamage;
                }
                fourthSkillCoulDownImage.fillAmount = 1;
            }
            fourthSkillCoulDownTXT.text = timer4.ToString("0.0");
            UpdateFillers(fourthSkillCoulDownImage, fourthSkillCd, timer4);
        }
    }

    private void UpdateFillers(Image fillerImage, float max, float current)
    {
        if (fillerImage != null)
            fillerImage.fillAmount = Mathf.MoveTowards(fillerImage.fillAmount, current / max, 10 * Time.deltaTime);
    }

    public void UpSkillOne()
    {
        currentClickUpgrades--;

        if (skillOneOpen)
        {
            fDamage += 0.1f;
            firstSkillCd -= 0.1f;
            if (firstSkillCd <= 0.5f)
            {
                firstSkillCd = 0.5f;
            }
            if (currentClickUpgrades <= 0)
            {
                button4.SetActive(false);
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
            }
        }
        else
        {
            skillOneOpen = true;
            if (currentClickUpgrades <= 0)
            {
                button4.SetActive(false);
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
            }
        }

    }

    public void UpSkillTwo()
    {
        currentClickUpgrades--;
        if (skillTwoOpen)
        {
            sDamage += 1f;
            secondSkillCd -= 0.1f;
            if (secondSkillCd <= 3)
            {
                secondSkillCd = 3;
            }
            if (currentClickUpgrades <= 0)
            {
                button4.SetActive(false);
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
            }
        }
        else
        {
            skillTwoOpen = true;
            if (currentClickUpgrades <= 0)
            {
                button4.SetActive(false);
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
            }
        }
    }

    public void UpSkillThree()
    {
        currentClickUpgrades--;
        if (skillThreeOpen)
        {
            tDamage += 0.3f;
            thirdSkillCd -= 0.1f;
            if (thirdSkillCd <= 7)
            {
                thirdSkillCd = 7;
            }
            if (currentClickUpgrades <= 0)
            {
                button4.SetActive(false);
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
            }
        }
        else
        {
            skillThreeOpen = true;
            if (currentClickUpgrades <= 0)
            {
                button4.SetActive(false);
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
            }
        }
    }

    public void UpSkillFour()
    {
        currentClickUpgrades--;
        if (skillFourOpen)
        {
            fourthSkillCd -= 1f;
            if (fourthSkillCd <= 30)
            {
                fourthSkillCd = 30;
            }
            if (currentClickUpgrades <= 0)
            {
                button4.SetActive(false);
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
            }
        }
        else
        {
            skillFourOpen = true;
            if (currentClickUpgrades <= 0)
            {
                button4.SetActive(false);
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
            }
        }
    }
}
