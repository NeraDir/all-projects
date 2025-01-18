using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RabbitJungleGameManager : MonoBehaviour
{
    public static int rabbitJungleBestRecord
    {
        get
        {
            if (PlayerPrefs.HasKey("rabbitJungleBestRecordSaveKey"))
            {
                return PlayerPrefs.GetInt("rabbitJungleBestRecordSaveKey");
            }
            return 1000;
        }
        set
        {
            PlayerPrefs.SetInt("rabbitJungleBestRecordSaveKey", value);
        }
    }

    public static int rabbitJungleScore;


    public static int rabbitJungleSkinSelectedIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("rabbitJungleSkinSelectedIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("rabbitJungleSkinSelectedIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("rabbitJungleSkinSelectedIndexSaveKey", value);
        }
    }

    public static int rabbitJunglePlatformsSpawnCountBegin
    {
        get
        {
            if (PlayerPrefs.HasKey("rabbitJunglePlatformsSpawnCountBeginSaveKey"))
            {
                return PlayerPrefs.GetInt("rabbitJunglePlatformsSpawnCountBeginSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("rabbitJunglePlatformsSpawnCountBeginSaveKey", value);
        }
    }

    public static string rabbitjunglegameSettingKey;

    public static float rabbitJunglePlatformWaittingTime 
    {
        get 
        {
            if (PlayerPrefs.HasKey("rabbitJunglePlatformWaittingTimeSaveKey"))
            {
                return PlayerPrefs.GetFloat("rabbitJunglePlatformWaittingTimeSaveKey");
            }
            return 5;
        }
        set 
        {
            PlayerPrefs.SetFloat("rabbitJunglePlatformWaittingTimeSaveKey", value);
        }
    }

    public static int rabbitJungleEggsSpawnPositionofZ
    {
        get
        {
            if (PlayerPrefs.HasKey("rabbitJungleEggsSpawnPositionofZSaveKey"))
            {
                return PlayerPrefs.GetInt("rabbitJungleEggsSpawnPositionofZSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("rabbitJungleEggsSpawnPositionofZSaveKey", value);
        }
    }

    public static float rabbitJunglePlatformAniamtorTime
    {
        get
        {
            if (PlayerPrefs.HasKey("rabbitJunglePlatformAniamtorTimeSaveKey"))
            {
                return PlayerPrefs.GetFloat("rabbitJunglePlatformAniamtorTimeSaveKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetFloat("rabbitJunglePlatformAniamtorTimeSaveKey", value);
        }
    }


    [SerializeField]
    private GameObject[] _jungleRabbitPlatforms;

    [SerializeField]
    private Text[] _rabbitJungleShowCurrentScore;

    [SerializeField]
    private GameObject _rabbitJungleGameResultScreen;

    private Vector3 _jungleRabbitPlatformsSpawnPosition = new Vector3(-4.61999989f, -2.74000001f, 0);

    private float _maxXDistanceBetweenPlatforms = 5;

    private float _minXDistanceBetweenPaltfroms = 8;

    private float _minYDistanceBetweenPlatforms = -7;

    private float _maxYDistanceBetweenPlatforms = 7;

    private int _rabbitJungleBeginSpawnPlatformsCount = 15;

    public static List<GameObject> PlatformsList = new List<GameObject>();

    private void Start()
    {
        rabbitJungleScore = 0;
        PlatformsList.Clear();
        RabbitJunglePlatformComponent.isPlatformEnded.AddListener(OnPaltformEnd);
        RabbitJunglePlatformComponent.playerDeath.AddListener(OnPalyerDeath);
        SpawnFirstPlatforms();
    }

    private void OnPalyerDeath() 
    {
        _rabbitJungleGameResultScreen.SetActive(true);
    }

    public void OnClickRestart() 
    {
        rabbitJunglePlatformWaittingTime = 5;
        rabbitJunglePlatformAniamtorTime = 1;
        rabbitJungleBestRecord += rabbitJungleScore;
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickMenu() 
    {
        rabbitJunglePlatformWaittingTime = 5;
        rabbitJunglePlatformAniamtorTime = 1;
        rabbitJungleBestRecord += rabbitJungleScore;
        SceneManager.LoadScene("MenuScene");
    }

    private void OnDestroy()
    {
        RabbitJunglePlatformComponent.isPlatformEnded.RemoveAllListeners();
        RabbitJunglePlatformComponent.playerDeath.RemoveAllListeners();
    }

    private void OnPaltformEnd(RabbitJunglePlatformComponent platform) 
    {
        platform.gameObject.SetActive(false);
        GameObject tempPlatform = platform.gameObject;
        PlatformsList.Remove(tempPlatform);
        PlatformsList.Add(tempPlatform);
        PlatformsList[PlatformsList.Count - 1].transform.position = new Vector3(PlatformsList[PlatformsList.Count - 2].transform.position.x + Random.Range(_minXDistanceBetweenPaltfroms,_maxXDistanceBetweenPlatforms), PlatformsList[PlatformsList.Count - 2].transform.position.y + Random.Range(_minYDistanceBetweenPlatforms, _maxYDistanceBetweenPlatforms),0);
        PlatformsList[PlatformsList.Count - 1].SetActive(true);
        rabbitJunglePlatformWaittingTime -= 0.05f;
        rabbitJunglePlatformAniamtorTime += 0.05f;
        if (rabbitJunglePlatformWaittingTime <= 0.5)
        {
            rabbitJunglePlatformWaittingTime = 0.5f;
            rabbitJunglePlatformAniamtorTime = 3f;
        }
    }

    private void OnApplicationQuit()
    {
        rabbitJunglePlatformAniamtorTime = 1;
        rabbitJunglePlatformWaittingTime = 5;
    }

    private void SpawnFirstPlatforms() 
    {
        for (int i = 0; i < _rabbitJungleBeginSpawnPlatformsCount; i++)
        {
            if (i == 0)
            {
                PlatformsList.Add(Instantiate(_jungleRabbitPlatforms[Random.Range(0, _jungleRabbitPlatforms.Length)], _jungleRabbitPlatformsSpawnPosition, Quaternion.Euler(-90,0,0)));
            }
            else
            {
                PlatformsList.Add(Instantiate(_jungleRabbitPlatforms[Random.Range(0, _jungleRabbitPlatforms.Length)], new Vector3(PlatformsList[PlatformsList.Count - 1].transform.position.x + Random.Range(_minXDistanceBetweenPaltfroms, _maxXDistanceBetweenPlatforms), PlatformsList[PlatformsList.Count - 1].transform.position.y + Random.Range(_minYDistanceBetweenPlatforms, _maxYDistanceBetweenPlatforms), 0), Quaternion.Euler(-90, 0, 0)));
            }
        }
    }

    private void LateUpdate()
    {
        foreach (var item in _rabbitJungleShowCurrentScore)
        {
            item.text = rabbitJungleScore.ToString() + "G";
        }
    }
}
