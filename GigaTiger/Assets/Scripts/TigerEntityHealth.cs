using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerEntityHealth : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> healthObbjectsInUI;

    private int health;

    public delegate void HealthIsOverDelegate();
    public static event HealthIsOverDelegate HealthIsOverEvent;




    private void OnEnable()
    {
        Food.DetectFoodEvent += Hill;
    }
    private void OnDisable()
    {
        Food.DetectFoodEvent -= Hill;
    }


    private void Start()
    {
        health = 3;
    }


    public void TakeDamage()
    {

        Debug.Log("Take Damage");

        if (health > 0)
        {
            health--;

            healthObbjectsInUI[health].SetActive(false);


            if (health == 0)
            {
                Debug.Log("GAME OVER");
                if (HealthIsOverEvent != null)
                    HealthIsOverEvent();
            }

        }
    }
    private void Hill()
    {
        //Debug.Log("Hill");

        if (health < 3)
        {
            healthObbjectsInUI[health].SetActive(true);
            health++;
            
        }


    }
}