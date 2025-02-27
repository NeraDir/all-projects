using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettysSpawnGrid : MonoBehaviour
{
    [Header("Границы спавна")]
    public Transform minPosition;  
    public Transform maxPosition;  

    [Header("Настройки грида")]
    public GameObject gridPrefab;  
    public int maxGrids = 5;       
    public float spawnDelay = 1f;  

    private IEnumerator Start()
    {
        
        while (true)
        {
            yield return new WaitForSeconds((spawnDelay / (ProfileData.BettysPlayerCurrentLevel + 1) ) < 0.2f ? 0.2f : (spawnDelay / (ProfileData.BettysPlayerCurrentLevel + 1)));
            Instantiate(gridPrefab, new Vector3(Random.Range(minPosition.position.x, maxPosition.position.x), minPosition.position.y, minPosition.position.z),Quaternion.identity);
        }
    }
}
