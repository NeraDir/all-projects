using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JarComponent : MonoBehaviour
{
    public Image fillingJarImage;

    public Image[] healthImnages;

    private int health;

    private void Start()
    {
        health = healthImnages.Length;
    }

    private void LateUpdate()
    {
        for (int i = 0; i < healthImnages.Length; i++) 
        {
            if (i < health)
            {
                healthImnages[i].gameObject.SetActive(true);
            }
            else
            {
                healthImnages[i].gameObject.SetActive(false);
            }
        }
        UpdateFillAmountOfImage();
    }

    private void UpdateFillAmountOfImage() 
    {
        fillingJarImage.fillAmount = Mathf.Lerp(fillingJarImage.fillAmount, RamGameManager.currentFillValue / RamGameManager.jarHealth, 10 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CrystallComponent crystall))
        {
            if (RamGameManager.needIndexCrystall == crystall.indexOfCrystall)
            {
                crystall.Destroye();
                RamGameManager.currentFillValue += crystall.Damage;
                if (RamGameManager.currentFillValue >= RamGameManager.jarHealth)
                {
                    FindObjectOfType<RamGameManager>().OpenResultpanel();
                }
            }
            else
            {
                crystall.Destroye();
                RamGameManager.currentFillValue -= crystall.Damage;
                if (RamGameManager.currentFillValue <= 0)
                {
                    health -= 1;
                    if (health <= 0)
                    {
                        FindObjectOfType<RamGameManager>().OpenLooseResultate();
                    }
                    RamGameManager.currentFillValue = 0;
                }
            }
        }
    }
}
