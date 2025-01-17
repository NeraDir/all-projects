using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadcomponent : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _traps;

    [SerializeField]
    private List<GameObject> _trapsTop;

    [SerializeField]
    private Transform[] _trapsPositionTop;

    [SerializeField]
    private Transform[] _trapsPositions;

    [SerializeField]
    private bool _withoutRoad;

    private void Start()
    {        
        if (_withoutRoad)
        {
            SpawnObjects(_trapsPositions,_traps);
            SpawnObjects(_trapsPositionTop,_trapsTop);
        }
        else
        {
            SpawnObjects(_trapsPositions,_traps);
        }
    }

    private void SpawnObjects(Transform[] positions, List<GameObject> whatToSpawn) 
    {
        List<GameObject> tempList = new List<GameObject>();
        List<GameObject> tempList2 = new List<GameObject>();
        foreach (var item in positions)
        {
            if (item.position.y > 1)
            {
                if (Random.Range(0, 2) != 0)
                {
                    GameObject tempObject = Instantiate(whatToSpawn[Random.Range(0, whatToSpawn.Count)], item.position, Quaternion.identity);
                    if (tempObject.name.Contains("coino"))
                    {
                        tempList.Clear();
                        tempObject.transform.position = new Vector3(tempObject.transform.position.x, tempObject.transform.position.y, tempObject.transform.position.z);
                    }
                    else
                    {
                        tempList.Add(tempObject);
                    }
                    if (tempList.Count >= 3)
                    {
                        int index = Random.Range(0, tempList.Count);
                        GameObject coinPrefab = _traps.Find(x => x.name.Contains("coino"));
                        Instantiate(coinPrefab, new Vector3(tempList[index].transform.position.x, tempList[index].transform.position.y, tempList[index].transform.position.z), Quaternion.identity);
                        Destroy(tempList[index].gameObject);
                        tempList.Clear();
                    }
                    tempObject.transform.parent = transform;
                }
            }
            else if (item.position.y < 1)
            {
                if (Random.Range(0, 2) != 0)
                {
                    GameObject tempObject = Instantiate(whatToSpawn[Random.Range(0, whatToSpawn.Count)], item.position, Quaternion.identity);
                    if (tempObject.name.Contains("coino"))
                    {
                        tempList2.Clear();
                        tempObject.transform.position = new Vector3(tempObject.transform.position.x, tempObject.transform.position.y + 1f, tempObject.transform.position.z);
                    }
                    else
                    {
                        tempList2.Add(tempObject);
                    }
                    if (tempList2.Count >= 3)
                    {
                        int index = Random.Range(0, tempList2.Count);
                        GameObject coinPrefab = _traps.Find(x => x.name.Contains("coino"));
                        Instantiate(coinPrefab, new Vector3(tempList2[index].transform.position.x, tempList2[index].transform.position.y + 1f, tempList2[index].transform.position.z), Quaternion.identity);
                        Destroy(tempList2[index].gameObject);
                        tempList2.Clear();
                    }
                    tempObject.transform.parent = transform;
                }
            }
        }
    }
}
