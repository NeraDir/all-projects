using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Image[] ballHeartsImages;

    [SerializeField]
    private Text[] ballStarsEarnedDisplay;

    [SerializeField]
    private Text[] ballReachedDistanceDispaly;

    [SerializeField]
    private Image ballResultScreen;

    [SerializeField]
    private Transform ballTransform;

    public static int earnedStars;

    public static float distance;

    public static int RecordStarsEarnedCount 
    {
        get
        {
            if (PlayerPrefs.HasKey("wallsStarsEarnedCountSaveKey"))
            {
                return PlayerPrefs.GetInt("wallsStarsEarnedCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("wallsStarsEarnedCountSaveKey", value);
        }
    }

    public static int wallsBeginSpawnCount
    {
        get
        {
            if (PlayerPrefs.HasKey("wallsBeginSpawnCountSaveKey"))
            {
                return PlayerPrefs.GetInt("wallsBeginSpawnCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("wallsBeginSpawnCountSaveKey", value);
        }
    }

    public static int ballHeartsCount = 3;

    public static string wallsDestroyerName;

    public static Vector3 beginBallPosition;

    public static int wallsDestroyerBeginScore
    {
        get
        {
            if (PlayerPrefs.HasKey("wallsDestroyerBeginScoreSaveKey"))
            {
                return PlayerPrefs.GetInt("wallsDestroyerBeginScoreSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("wallsDestroyerBeginScoreSaveKey", value);
        }
    }

    [SerializeField]
    private GameObject[] roadsPrefab;

    private bool gameStarted;

    private void Awake()
    {
        earnedStars = 0;
        beginBallPosition = ballTransform.position;
        ballHeartsCount = 3;
        Time.timeScale = 1;
        WallsSpawnNewRoadComponent.spawnNewRoad.AddListener(SpawnRoad);
        gameStarted = true;
    }

    private void LateUpdate()
    {
        if (!gameStarted)
            return;
        if (ballHeartsCount <= 0)
        {
            ballResultScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
            gameStarted = false;
        }
        distance = Vector3.Distance(ballTransform.position, beginBallPosition);
        foreach (var item in ballReachedDistanceDispaly)
        {
            item.text = (distance).ToString("0.0") + "m";
        }
        foreach (var item in ballStarsEarnedDisplay)
        {
            item.text = "X" + earnedStars.ToString();
        }
        if (earnedStars > RecordStarsEarnedCount)
        {
            RecordStarsEarnedCount = earnedStars; 
        }
        if (distance > DestroyerMenuController.RecordReachedDistance)
        {
            DestroyerMenuController.RecordReachedDistance = distance;
        }
        for (int i = 0; i < ballHeartsImages.Length; i++)
        {
            if (i >= ballHeartsCount)
            {
                ballHeartsImages[i].transform.DOScale(Vector3.zero, 0.5f);
            }
        }
    }

    public void OnClickMenuLoad() 
    {
        SceneManager.LoadScene("WallsDestroyerMenuScene");
    }

    public void OnClickRestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        WallsSpawnNewRoadComponent.spawnNewRoad.RemoveAllListeners();
    }

    private void SpawnRoad(Transform positio) 
    {
        Instantiate(roadsPrefab[Random.Range(0, roadsPrefab.Length)], positio.position, roadsPrefab[0].transform.rotation);
    }
}
