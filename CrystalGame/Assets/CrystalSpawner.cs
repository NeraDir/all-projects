using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    [SerializeField]
    private List<CrystalSpawnLine> crystalSpawnLines;

    [SerializeField]
    private List<Crystal> crystalPrefabs;

    [SerializeField]
    private float crystalsMoveSpeed;

   

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        //Direction newDirection = Direction.Right;
        //crystalSpawnLines[0].Init(crystalPrefabs, newDirection, crystalsMoveSpeed);
        //crystalSpawnLines[0].StartSpawn();
        float tempMaxSpeed = crystalsMoveSpeed;

        for (int i = 0; i < PlayerDatasSaver.countOfPressedNext; i++)
        {
            tempMaxSpeed += crystalsMoveSpeed;
        }


        for (int i = 0; i < crystalSpawnLines.Count; i++)
        {
            Direction newDirection = Direction.Right;

            if (i % 2 != 0)
                newDirection = Direction.Left;
            
            crystalSpawnLines[i].Init(crystalPrefabs, newDirection, tempMaxSpeed);


            crystalSpawnLines[i].StartSpawn();
        }
        
    }

}
