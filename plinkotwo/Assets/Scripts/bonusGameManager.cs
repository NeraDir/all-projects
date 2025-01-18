using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class bonusGameManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPosition;

    [SerializeField]
    private GameObject _ballPrefab;

    [SerializeField]
    private Material[] _ballMaterials;

    public static int starsCount;

    [SerializeField]
    private Text[] _starsTxt;

    private bool _bonusStarted;

    [SerializeField]
    private GameObject _sresultScreen;

    private void Start()
    {
        Time.timeScale = 1;
        starsCount = 0;
        for (int i = 0; i < Random.Range(10, 30); i++)
        {
            GameObject tempBall = Instantiate(_ballPrefab, new Vector3(Random.Range(_spawnPosition[0].position.x, _spawnPosition[1].position.x), _spawnPosition[0].position.y, _spawnPosition[0].position.z), Quaternion.identity);
            tempBall.GetComponent<MeshRenderer>().material = _ballMaterials[Random.Range(0, _ballMaterials.Length)];
        }
        _bonusStarted = true;
    }

    private void LateUpdate()
    {
        if (!_bonusStarted)
            return;
        foreach (var item in _starsTxt)
        {
            item.text = "x" + starsCount.ToString("0");
        }
        if (FindObjectOfType<bonusBall>() == null)
        {
            _sresultScreen.SetActive(true);
        }
    }

    public void OnClickMenu() 
    {
        gameManager.maxStarsCount += starsCount;
        SceneManager.LoadScene("menuScene");
    }
}
