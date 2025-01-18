using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject rocket;

    [SerializeField]
    private GameObject ship;

    [SerializeField]
    private Transform[] spawnPositions;

    [SerializeField]
    private TMP_Text[] _showCurrentValue;

    [SerializeField]
    private TMP_Text[] _showCurrentRecord;

    [SerializeField]
    private TMP_Text[] _showCurrentLifeTime;

    private static CinemachineVirtualCamera _virtualCamera;

    public static int DestroyedShipsRecord
    {
        get
        {
            if (PlayerPrefs.HasKey("DestroyedShipsRecordValue")) 
            {
                return PlayerPrefs.GetInt("DestroyedShipsRecordValue");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("DestroyedShipsRecordValue", value);
        }
    }

    public static float RecordLifeTime 
    {
        get
        {
            if (PlayerPrefs.HasKey("RecordLifeTime"))
            {
                return PlayerPrefs.GetFloat("RecordLifeTime");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("RecordLifeTime", value);
        }
    }

    public static float currentLifeTime;

    public static int destroyedShipsCurrentValue;

    public static bool gameStarted;

    private IEnumerator Start()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
           gameStarted = true;
        currentLifeTime = 0;
        destroyedShipsCurrentValue = 0;
        while (true)
        {
            yield return new WaitForSeconds(10);
            Instantiate(ship, spawnPositions[Random.Range(0, spawnPositions.Length)].position, Quaternion.identity);
        }
    }

    private void LateUpdate()
    {
        if (gameStarted) 
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Instantiate(rocket);
            }
            if (destroyedShipsCurrentValue > DestroyedShipsRecord)
            {
                DestroyedShipsRecord = destroyedShipsCurrentValue;
            }
            currentLifeTime += Time.deltaTime;
            if (currentLifeTime > RecordLifeTime)
            {
                RecordLifeTime = currentLifeTime;
            }

            foreach (var item in _showCurrentValue)
            {
                item.text = "CURRENT: " + destroyedShipsCurrentValue.ToString("0");
            }
            foreach (var item in _showCurrentRecord)
            {
                item.text = "RECORD: " + DestroyedShipsRecord.ToString("0");
            }
            foreach (var item in _showCurrentLifeTime)
            {
                item.text = "LIFE TIME: " + currentLifeTime.ToString("0") + "s";
            }
        }
    }

    public static void NoiseCam() 
    {
        _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin> ().m_AmplitudeGain = 2f;
        FindObjectOfType<GameManager>().UnUsing();
    }

    public void UnUsing() 
    {
        StartCoroutine(UnNoiseCam());
    }

    public static IEnumerator UnNoiseCam() 
    {
        yield return new WaitForSeconds(0.1f);
        _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
    }

    public void OnClickPlayAgain() 
    {
        PlaneDataContainer.PlanesCoins += destroyedShipsCurrentValue * 10;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickOpenMenu() 
    {
        PlaneDataContainer.PlanesCoins += destroyedShipsCurrentValue * 10;
        SceneManager.LoadScene("MenuScene");
    }
}
