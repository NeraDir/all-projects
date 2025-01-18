using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawnmanager : MonoBehaviour
{
    public float startTime = 69f;
    public float TimeToSpawn = 2f;
    public List<Enemy> enemies = new();
    public List<Transform> spawnPoints = new();
    public GameScript scrGame;

    public Enemy currentCell;

    [HideInInspector] public AllSellebleItems ShopItems;

    public void Start()
    {
        ShopItems = FindObjectOfType<AllSellebleItems>();
        Player.UpdateTime += UpdTime;
        UpdTime();
        Instantiate(ShopItems.GetEquipedItem().itemModal, Vector3.zero, Quaternion.identity);
    }

    public void UpdTime()
    {
        SpawnEntity();
        scrGame.UpdateTime(startTime);
        startTime -= 5f;
    }

    public Vector3 GetRandomPointOnMap()
    {
        int ff = Random.Range(0, spawnPoints.Count);

        return spawnPoints[ff].position;
    }

    private void SpawnEntity()
    {
        int Rand = Random.Range(0, enemies.Count);

        Enemy buff = Instantiate(enemies[Rand], GetRandomPointOnMap(), enemies[Rand].gameObject.transform.rotation);
        buff.isSpawned = true;
        currentCell = buff;
        scrGame.FindPhonto.sprite = currentCell.MySprite;
    }

    private void OnDestroy()
    {
        Player.UpdateTime -= UpdTime;
    }
}
