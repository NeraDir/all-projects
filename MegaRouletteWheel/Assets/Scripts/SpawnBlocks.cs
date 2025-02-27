using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpawnBlocks : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private Image prefab;

    [SerializeField] private Transform[] _spawnPositions;

    public static Action action;
    public static bool spawn;

    private IEnumerator Start()
    {

        while (true)
        {
            if (spawn)
            {
                for (int i = 0; i < 15; i++)
                {
                    Spawn();
                }
                action?.Invoke();
                
            }
            yield return null;
        }
    }

    private void Spawn()
    {
        Image newImage = Instantiate(prefab, new Vector3(Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x), _spawnPositions[0].position.y, _spawnPositions[0].position.z), Quaternion.Euler(0, 0, Random.Range(-360, 360)), _spawnPositions[0]);
        float rndS = Random.Range(1, 1.5f);
        newImage.transform.localScale = new Vector3(rndS, rndS, rndS);
        newImage.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
