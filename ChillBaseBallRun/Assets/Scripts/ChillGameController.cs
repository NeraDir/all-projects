using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChillGameController : MonoBehaviour
{
    public static float ChillBaseMaxDistanceReached {
        get{
            if (PlayerPrefs.HasKey("ChillBaseMaxReachedDistanceKey"))
            {
                return PlayerPrefs.GetFloat("ChillBaseMaxReachedDistanceKey");
            }
            return 0.0f;
        }
        set {
            PlayerPrefs.SetFloat("ChillBaseMaxReachedDistanceKey", value);
        }
    }

    public static int chillBaseGameStartSpeed
    {
        get
        {
            if (PlayerPrefs.HasKey("chillBaseGameStartSpeedKey"))
            {
                return PlayerPrefs.GetInt("chillBaseGameStartSpeedKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("chillBaseGameStartSpeedKey", value);
        }
    }

    public static string chillBaseGameSettings;

    public static int chillBaseGameEnableUi
    {
        get
        {
            if (PlayerPrefs.HasKey("chillBaseGameEnableUiKey"))
            {
                return PlayerPrefs.GetInt("chillBaseGameEnableUiKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("chillBaseGameEnableUiKey", value);
        }
    }

    private float _chillBaseCurrentDistance;

    [SerializeField]
    private GameObject[] _chillPlatforms;

    [SerializeField]
    private GameObject _chillStartPlatform;

    private List<GameObject> _chillPlatformsList = new List<GameObject>();

    public static UnityEvent chillBallIsDeath = new UnityEvent();

    public static UnityEvent chillSpawnPlatforms = new UnityEvent();

    private Vector3 _chillBaseBallStartPosition;

    [SerializeField]
    private Transform _chillBaseBallTransform;

    [SerializeField]
    private Text[] _chillDistanceShow;

    [SerializeField]
    private GameObject _chillResultScreen;

    private bool _isChillEnd;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            OnSpawnPlatform();
        }
        chillSpawnPlatforms.AddListener(OnSpawnPlatform);
        chillBallIsDeath.AddListener(OnBallDeath);
    }

    private void OnBallDeath()
    {
        _isChillEnd = true;
        _chillResultScreen.SetActive(true);
    }

    private void LateUpdate()
    {
        if (_isChillEnd)
            return;
        _chillBaseCurrentDistance = Vector3.Distance(_chillBaseBallTransform.position, _chillBaseBallStartPosition);
        if (_chillBaseCurrentDistance > ChillBaseMaxDistanceReached)
        {
            ChillBaseMaxDistanceReached = _chillBaseCurrentDistance;
        }
        foreach (var item in _chillDistanceShow)
        {
            item.text = _chillBaseCurrentDistance.ToString("0.0") + "m";
        }
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu() 
    {
        SceneManager.LoadScene("ChillBaseMenu");
    }

    private void OnDestroy()
    {
        chillSpawnPlatforms.RemoveListener(OnSpawnPlatform);
        chillBallIsDeath.RemoveListener(OnBallDeath);
    }

    private void OnSpawnPlatform() 
    {
        if (_chillPlatformsList.Count > 1)
            _chillPlatformsList.Add(Instantiate(_chillPlatforms[Random.Range(0, _chillPlatforms.Length)], new Vector3(0, Random.Range(-2, 2), _chillPlatformsList[_chillPlatformsList.Count - 1].transform.position.z + Random.Range(5, 8)), Quaternion.identity));
        else
            _chillPlatformsList.Add(Instantiate(_chillStartPlatform, new Vector3(0,0, -2.91f),Quaternion.identity));
    }
}
