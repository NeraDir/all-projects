using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class candyManRunning : MonoBehaviour
{
    private int _roadsCountReached;

    [SerializeField]
    private candyRoadGeneration _ropadgen;

    [SerializeField]
    private TMP_Text[] _candyScoreTxt;

    private Rigidbody _groundBody;

    private int _iterations;

    private int _score;

    [SerializeField]
    private GameObject endPage;

    public int Score => _score;

    private void Start()
    {
        _groundBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (_groundBody == null)
            return;

        Vector3 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _groundBody.velocity = new Vector3( -direction.x * 1, 0, 10);
        foreach (var item in _candyScoreTxt)
        {
            item.text = _score.ToString("0");
        }

        if (_score > CandyMenu.CandybestScore)
        {
            CandyMenu.CandybestScore = _score;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CandyRoadComponent road))
        {
            RoadUpdater();
        }
        else if (other.TryGetComponent(out CandyLoot loot))
        {
            OnLootScore(loot);
        }
        else if (other.TryGetComponent(out EndComponent ender))
        {
            End();
            _groundBody.velocity = Vector3.zero;
            _groundBody.isKinematic = true;
        }
    }

    public void Next() 
    {
        candyRoadGeneration.NeedInterationsSave += 2;
        SceneManager.LoadScene(1);
    }

    public void Restart() 
    {
        candyRoadGeneration.NeedInterationsSave = 0;
        SceneManager.LoadScene(1);
    }

    public void Menu() 
    {
        candyRoadGeneration.NeedInterationsSave = 0;
        SceneManager.LoadScene(3);
    }

    private void OnLootScore(CandyLoot lot) 
    {
        _score += lot.Score;
        lot.Destroye();
    }

    private void End() 
    {
        endPage.SetActive(true);
    }

    private void RoadUpdater() 
    {
        _roadsCountReached++;
        if (_roadsCountReached == 3)
        {
            _iterations++;
            if (_iterations != _ropadgen.NeedInterationsEnd)
            {
                for (int i = 0; i < 2; i++)
                {
                    _ropadgen.RoadsPool[0].SetActive(false);
                    _ropadgen.RoadsPool[0].transform.position = new Vector3(0, 0, _ropadgen.RoadsPool[_ropadgen.RoadsPool.Count - 1].transform.position.z + _ropadgen.RoadDistance);
                    GameObject tempRoad = _ropadgen.RoadsPool[0];
                    _ropadgen.RoadsPool.Remove(_ropadgen.RoadsPool[0]);
                    _ropadgen.RoadsPool.Add(tempRoad);
                    for (int j = 0; j < _ropadgen.RoadsPool.Count; j++)
                    {
                        _ropadgen.RoadsPool[j].GetComponent<CandyRoadComponent>().RoadIndex = j;
                        _ropadgen.RoadsPool[j].SetActive(true);
                    }
                }
                _roadsCountReached = 0;
                _roadsCountReached++;
            }
            else
            {
                _ropadgen.EndPlatform.transform.position = new Vector3(0, 0, _ropadgen.RoadsPool[_ropadgen.RoadsPool.Count - 1].transform.position.z + _ropadgen.RoadDistance / 2.2f);
                _ropadgen.EndPlatform.SetActive(true);
            }
        }
    }
}
