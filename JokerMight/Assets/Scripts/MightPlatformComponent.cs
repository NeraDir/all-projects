using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MightPlatformComponent : MonoBehaviour
{
    public GameObject[] roadPieces;

    public MeshRenderer roadMesh;

    public Material[] roadMaterials;

    public Transform roadPieceSpawnPosition;

    public bool isSpawner = false;

    public int indexOfPlatform;

    public List<GameObject> spawnedPieces = new List<GameObject>();

    private void Start()
    {
        indexOfPlatform = Random.Range(0, roadMaterials.Length);
        roadMesh.material = roadMaterials[indexOfPlatform];
        StartCoroutine(SpawningRoadPiece());
    }

    private IEnumerator SpawningRoadPiece() 
    {
        while (true)
        {
            if (isSpawner)
            {
                MightPieceOfRoad tempRoad = Instantiate(roadPieces[Random.Range(0, roadPieces.Length)].GetComponent<MightPieceOfRoad>(), roadPieceSpawnPosition.position, roadPieceSpawnPosition.rotation);
                tempRoad.transform.localScale = roadPieces[0].transform.localScale;
                tempRoad.transform.rotation = roadPieces[0].transform.rotation;
                tempRoad.mightPlatform = this;
                spawnedPieces.Add(tempRoad.gameObject);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
