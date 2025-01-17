using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int crazyEnemiesConstantCount
    {
        get
        {
            if (PlayerPrefs.HasKey("crazyEnemiesConstantCountSave"))
            {
                return PlayerPrefs.GetInt("crazyEnemiesConstantCountSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("crazyEnemiesConstantCountSave", value);
        }
    }

    public static string crazyPlayerName;

    public static int crazyLaunchCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("crazyLaunchCountsSave"))
            {
                return PlayerPrefs.GetInt("crazyLaunchCountsSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("crazyLaunchCountsSave", value);
        }
    }

    public static float BestPlayerTime 
    {
        get 
        {
            if (PlayerPrefs.HasKey("CrazyBestTimePlayer"))
            {
                return PlayerPrefs.GetFloat("CrazyBestTimePlayer");
            }
            return 0f;
        }
        set 
        {
            PlayerPrefs.SetFloat("CrazyBestTimePlayer",value);
        }
    }

    public static int currentScore;

    [SerializeField]
    private TMP_Text showTimer;

    public float LifeTime;

    private float _timer;

    [SerializeField]
    private CrystallsParamConfig _crystallsConfig;

    public CrystallsParamConfig CrystallConfig => _crystallsConfig;

    [SerializeField]
    private Image _nextCrystallImage;

    [SerializeField]
    private TMP_Text[] _showCurrentScore;

    [SerializeField]
    private Image _crystallPref;

    [SerializeField]
    private Image _line;

    private Image _currentCrystall;

    [SerializeField]
    private Joystick _joystick;

    private CrystallData _nextcrystallData;

    [SerializeField]
    private GameObject _endGamePanel;

    private void Start()
    {
        currentScore = 0;
        _currentCrystall = Instantiate(_crystallPref, _line.transform.position, Quaternion.identity,_line.transform.parent);
        _currentCrystall.GetComponent<CrystallComponent>().Init(_crystallsConfig.CrystallsDatas[Random.Range(0, _crystallsConfig.CrystallsDatas.Count)]);
        _nextcrystallData = _crystallsConfig.CrystallsDatas[Random.Range(0, _crystallsConfig.CrystallsDatas.Count)];
        _nextCrystallImage.sprite = _nextcrystallData.Sprite;
        _timer = LifeTime;
    }

    private void LateUpdate()
    {
        BestPlayerTime = currentScore > BestPlayerTime ? currentScore : BestPlayerTime;
        foreach (var item in _showCurrentScore)
        {
            item.text = currentScore.ToString("0");
        }
        
        _timer -= Time.deltaTime;
        showTimer.text = _timer.ToString("0") + "s";
        if (_timer <= 0)
        {
            _endGamePanel.SetActive(true);
            _timer = 0;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButton(0))
        {
            if (_currentCrystall == null)
                return;
            if (_currentCrystall.GetComponent<RectTransform>().position.x > 1.8f || _currentCrystall.GetComponent<RectTransform>().position.x < -1.8f)
            {
                _currentCrystall.GetComponent<Rigidbody>().AddForce(-_currentCrystall.GetComponent<Rigidbody>().velocity * 1.8f,ForceMode.Impulse);
                return;
            }
            Debug.Log(_currentCrystall.GetComponent<RectTransform>().position.x);
            _currentCrystall.GetComponent<Rigidbody>().velocity = new Vector3(_joystick.Horizontal * 1.7f, _currentCrystall.GetComponent<Rigidbody>().velocity.y, 0);


        }
        else if (Input.GetMouseButtonUp(0))
        {
            _currentCrystall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _currentCrystall = null;
            _nextcrystallData = _crystallsConfig.CrystallsDatas[Random.Range(0, _crystallsConfig.CrystallsDatas.Count)];
            _nextCrystallImage.sprite = _nextcrystallData.Sprite;
            Invoke(nameof(SpawnNew), 1);
        }
    }

    private void SpawnNew() 
    {
        _currentCrystall = Instantiate(_crystallPref, _line.transform.position, Quaternion.identity, _line.transform.parent);
        _currentCrystall.GetComponent<CrystallComponent>().Init(_nextcrystallData);

    }

    public void OnRestartButtonPressed() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnMenuButtonPressed() 
    {
        SceneManager.LoadScene("Menu");
    }
}
