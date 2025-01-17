using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject aviatorPlanes;

    public List<float> positionsOfX = new List<float>();

    public static int PlayerHealth;

    public GameObject playerLoose;

    public GameObject playerWin;

    private IEnumerator Start()
    {
        aviGameController.isGameEnd = true;
        yield return new WaitForSeconds(1.9f);
        aviGameController.isGameEnd = false;
        PlayerHealth = 30;
        /*while (true) 
        {
            yield return new WaitForSeconds(aviGameController.aviatorSpawningTime);
            if (!aviGameController.isGameEnd)
            {
                foreach (var item in positionsOfX)
                {
                    Instantiate(aviatorPlanes, new Vector3(item, -5.9f, 0), aviatorPlanes.transform.rotation);
                }
            }
        }*/
    }

    private void LateUpdate()
    {
        if (!aviGameController.isGameEnd)
        {
            if (PlayerHealth <= 0)
            {
                playerLoose.SetActive(true);
                playerWin.SetActive(false);
                aviGameController.isGameEnd = true;
            }
            else if (aviaEnemie.enemieHealth <= 0)
            {
                playerLoose.SetActive(false);
                playerWin.SetActive(true);
                aviGameController.isGameEnd = true;
            }
        }
        
    }

    public void OnCLickRestart() 
    {
        SceneManager.LoadScene("game");
    }

    public void OnClickMenu() 
    {
        SceneManager.LoadScene("menu");
    }
}
