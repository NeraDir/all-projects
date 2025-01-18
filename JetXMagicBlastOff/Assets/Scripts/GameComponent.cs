using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameComponent : MonoBehaviour
{
    public static bool isGameLaunched;

    public static float enemieHealth;

    public static int score;

    [SerializeField]
    private GameObject enemieRocket;

    [SerializeField]
    private GameObject resultScreen;

    [SerializeField]
    private Transform[] enemiesSpawnPositions;

    [SerializeField]
    private Text[] scoreTXT;

    private float timeToUpEnemieHealth = 0;

    private void Start()
    {
        Time.timeScale = 1;
        isGameLaunched = false;
        score = 0;
        timeToUpEnemieHealth = 0;
        StartCoroutine(EnemieRocketsSpawner());
        enemieHealth = 1;
    }

    private IEnumerator EnemieRocketsSpawner() 
    {
        float[] enemieSpawnTime = new float[3];
        enemieSpawnTime[0] = Random.Range(0.5f, 3);
        enemieSpawnTime[1] = Random.Range(0.25f, 4);
        enemieSpawnTime[2] = Random.Range(1f, 2f);
        while (true)
        {
            yield return new WaitForSeconds(enemieSpawnTime[Random.Range(0, enemieSpawnTime.Length)]);
            GameObject tempEnemie = Instantiate(enemieRocket, enemiesSpawnPositions[Random.Range(0, enemiesSpawnPositions.Length)].position, Quaternion.identity, enemiesSpawnPositions[0].parent);
            tempEnemie.transform.SetSiblingIndex(0);
        }
    }

    private void LateUpdate()
    {
        if (score > MenuComponent.BestScore)
        {
            MenuComponent.BestScore = score;
        }

        timeToUpEnemieHealth += Time.deltaTime;
        if (timeToUpEnemieHealth >= 6)
        {
            enemieHealth += 2;
            timeToUpEnemieHealth = 0;
        }

        if (isGameLaunched)
        {
            Time.timeScale = 0;
            resultScreen.SetActive(true);
        }

        foreach (var item in scoreTXT)
        {
            item.text = score.ToString("0");
        }
    }

    public void OnClickRestart() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickMenu() 
    {
        SceneManager.LoadScene("Menu");
    }
}
