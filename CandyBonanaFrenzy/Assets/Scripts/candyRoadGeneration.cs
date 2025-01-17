using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candyRoadGeneration : MonoBehaviour
{
    public List<GameObject> RoadsPool = new List<GameObject>();

    [SerializeField]
    private float _needDistance;

    [SerializeField]
    private int _startRoadCount;

    [SerializeField]
    private GameObject _roadPrefab;

    [SerializeField]
    private GameObject _endSegment;

    private GameObject endSegment;

    public static int NeedInterationsSave 
    {
        get 
        {
            if (PlayerPrefs.HasKey("CandyInterationSave"))
                return PlayerPrefs.GetInt("CandyInterationSave");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("CandyInterationSave", value);
        }
    }

    public float RoadDistance => _needDistance;

    [SerializeField]
    private int _needInterationsForEnd;

    public int NeedInterationsEnd => NeedInterationsSave;

    public GameObject EndPlatform => endSegment;

    private void Awake()
    {
        if (NeedInterationsSave == 0)
        {
            NeedInterationsSave = _needInterationsForEnd;
        }
        GenerateRoad();
    }

    [ContextMenu("Generate")]
    private void GenerationRoadTemp() 
    {
        GenerateRoad();
    }

    private void GenerateRoad() 
    {
        if (RoadsPool.Count > 0)
        {
            foreach (var item in RoadsPool)
            {
                Destroy(item);
            }
            RoadsPool.Clear();
        }

        for (int i = 0; i < _startRoadCount; i++)
        {
            if (RoadsPool.Count < 1)
            {
                GameObject tempRoad = Instantiate(_roadPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                tempRoad.GetComponent<CandyRoadComponent>().RoadIndex = i;
                RoadsPool.Add(tempRoad);

            }
            else
            {
                GameObject tempRoad = Instantiate(_roadPrefab, new Vector3(0, 0, RoadsPool[RoadsPool.Count - 1].transform.position.z + _needDistance), Quaternion.identity);
                tempRoad.GetComponent<CandyRoadComponent>().RoadIndex = i;
                RoadsPool.Add(tempRoad);
            }
        }

        endSegment =  Instantiate(_endSegment, new Vector3(0, 0, 0), Quaternion.identity);
        endSegment.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        NeedInterationsSave = 0;
    }
}
