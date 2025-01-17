using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{    
    public GameObject[] prefabsToSpawn;
    public Transform[] spawnPositions;

    public GameObject BossObj;
    public Transform SpawnPosBoss;
    public Text RoundCounter;
    // закинуть префаб босса и чекать его хп
    public float BossHealth = 20f;
    public float enemyhealth=5f;
    public static int _counterRound = 1;
    private Text _textHPBoss;
    private Text[] _textHPEnemy;
    private float spawnInterval = 7f;
    private float _percent = 2f;
    void Start()
    {

        FillArrayTextHP();
        RoundCounter.text = "Round " + _counterRound.ToString();             
        InvokeRepeating("SpawnObjAgain", 0f, spawnInterval);
        
    }

    private void FillArrayTextHP()
    {
        _textHPEnemy = new Text[prefabsToSpawn.Length];
        for (int i = 0; i < _textHPEnemy.Length; i++)
        {
            _textHPEnemy[i] = prefabsToSpawn[i].GetComponentInChildren<Text>();
            _textHPEnemy[i].text = enemyhealth.ToString();
        }
        _textHPBoss = BossObj.GetComponentInChildren<Text>();
        _textHPBoss.text = BossHealth.ToString();
    }
    void SpawnObjAgain()
    {
        
        if (!EndGame.endGame)
        {
            if (CollisionController.NextLevel)
            {
                 //спавнится босс
                NextLevel();
                SpawnPrefab(BossObj, SpawnPosBoss);
                CollisionController.NextLevel = false;
                _counterRound++;
                RoundCounter.text = "Round " + _counterRound.ToString();
            }
            for (int i = 0; i < prefabsToSpawn.Length; i++)
            {
                SpawnPrefab(prefabsToSpawn[i], spawnPositions[i]);
            }
        }
        
    }
    private void SpawnPrefab(GameObject prefab, Transform spawnPosition)
    {
        Instantiate(prefab, spawnPosition.position, Quaternion.identity, spawnPosition.transform);
    }

    public void NextLevel()
    {
        for (int i = 0; i < _textHPEnemy.Length; i++)
            _textHPEnemy[i].text = (float.Parse(_textHPEnemy[i].text) + _percent).ToString();
        _textHPBoss.text = (float.Parse(_textHPBoss.text) + _percent).ToString();
    }

}
