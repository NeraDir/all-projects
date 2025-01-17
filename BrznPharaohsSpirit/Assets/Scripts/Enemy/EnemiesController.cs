using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public RectTransform Parrent;
    public Enemy enemyPrefab;
    public float TimeToCreateEnemy;
    public List<EnemySTR> Enemies = new();

    float Timer = 0f;

    private void Update()
    {
        Timer += Time.deltaTime;

        if(Timer >= TimeToCreateEnemy)
        {
            Enemy buff = Instantiate(enemyPrefab, Parrent);
            buff.SwitchOnAttack(Enemies[Random.Range(0, Enemies.Count)]);
            Timer = 0f;
        }
    }
}

[System.Serializable]
public struct EnemySTR
{
    public RectTransform EnemyRect;
    public RectTransform FromPos;
    public RectTransform TargetRect;
}