using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopGameManager : MonoBehaviour
{
    public static int popBestScore 
    {
        get
        {
            if (PlayerPrefs.HasKey("popBestScoreSave"))
                return PlayerPrefs.GetInt("popBestScoreSave");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("popBestScoreSave", value);
        }
    }

    [SerializeField]
    private Animator _gameAnimator;

    [SerializeField]
    private GameObject _coapObject;

    [SerializeField]
    private GameObject _tableObject;

    [SerializeField]
    private GameObject[] _fruitsObjects;

    [SerializeField]
    private TMP_Text[] _popScoreDisplay;

    private float _timeSpawn = 2;

    public static int popScore = 0;

    [SerializeField]
    private GameObject _gameEndPanel;

    private PopCoap _popCop;

    private bool _gameStarted;

    private void Awake()
    {
        popScore = 0;
        _gameStarted = true;
        _popCop = FindAnyObjectByType<PopCoap>();
    }

    private IEnumerator StartSpawn() 
    {
        int countSpawned = 0;
        while (true) 
        {
            Instantiate(_fruitsObjects[Random.Range(0, _fruitsObjects.Length)], new Vector3(_coapObject.transform.position.x, _coapObject.transform.position.y + 100, _coapObject.transform.position.z), Quaternion.identity);
            countSpawned++;
            if (countSpawned >= 10)
            {
                float yG = Physics.gravity.y - 0.25f;
                Physics.gravity = new Vector3(0, yG, 0);
                countSpawned = 0;
            }
            yield return new WaitForSeconds(_timeSpawn);
        }
    }

    private void LateUpdate()
    {
        foreach (var item in _popScoreDisplay)
        {
            item.text = popScore.ToString();
        }
        if (_gameStarted)
        {
            if (_popCop.GetCoapFillState())
            {
                if (popScore > popBestScore)
                {
                    popBestScore = popScore;
                }
                _gameEndPanel.SetActive(true);
            }
        }
    }

    public void PopMenuLoad() 
    {
        SceneManager.LoadScene("PopMenuScene");
    }

    public void PopGameReLoad() 
    {
        SceneManager.LoadScene("PopGame");
    }

    public void OnAnimationNullableParent() 
    {
        _coapObject.transform.parent = _tableObject.transform;
    }

    public void OnAnimationGameInitialization() 
    {
        Destroy(_gameAnimator);
        Physics.gravity = new Vector3(0, -5,0);
        _coapObject.AddComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(StartSpawn());
    }
}
