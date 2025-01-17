using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    [SerializeField] private GameObject panel1;



    private void Awake()
    {
        if (PlayerPrefs.GetInt("Training") == 0)
        {
            Time.timeScale = 0;
            panel1.SetActive(true);
            StartCoroutine(Close());
        }
       
    }

   

    private void OnMouseDown()
    {
        panel1.SetActive(false);

        PlayerPrefs.SetInt("Training", 1);
    }

    private IEnumerator Close()
    {
        yield return new WaitForSecondsRealtime(5);
        panel1.SetActive(false);
        Time.timeScale = 1;

        PlayerPrefs.SetInt("Training", 1);
    }
}
