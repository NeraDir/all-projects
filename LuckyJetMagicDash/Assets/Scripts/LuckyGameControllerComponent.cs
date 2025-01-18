using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LuckyGameControllerComponent : MonoBehaviour
{

    public static int LuckyPlayerBestScore 
    {
        get
        {
            if (PlayerPrefs.HasKey("LuckyPlayerBestScoreSaveKey"))
            {
                return PlayerPrefs.GetInt("LuckyPlayerBestScoreSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("LuckyPlayerBestScoreSaveKey", value);
        }
    }

    public static int LuckyGameStartCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("LuckyGameStartCountsSaveKey"))
            {
                return PlayerPrefs.GetInt("LuckyGameStartCountsSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("LuckyGameStartCountsSaveKey", value);
        }
    }

    public static int LuckyPlayerSeesGameHowToPlay 
    {
        get
        {
            if (PlayerPrefs.HasKey("LuckyLuckyPlayerSeesGameHowToPlaySaveKey"))
            {
                return PlayerPrefs.GetInt("LuckyLuckyPlayerSeesGameHowToPlaySaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("LuckyLuckyPlayerSeesGameHowToPlaySaveKey", value);
        }
    }

    public static int LuckyGameCurrentScore;

    public static string LuckyGameInitializationKey;

    public static int LuckyPlayerHeartsCount;

    public static GameObject LuckyLastSpawnedObject;

    public static int LuckyGameInitializationCount
    {
        get
        {
            if (PlayerPrefs.HasKey("LuckyGameInitializationCountSaveKey"))
            {
                return PlayerPrefs.GetInt("LuckyGameInitializationCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("LuckyGameInitializationCountSaveKey", value);
        }
    }

    public static UnityEvent LuckyGamePlayerTriggeredLevelPiece = new UnityEvent();

    [SerializeField]
    private GameObject[] luckyAsteroidsObjects;

    [SerializeField]
    private GameObject luckyPieceWithRing;

    [SerializeField]
    private GameObject[] luckyPlayerHeartsObjects;

    [SerializeField]
    private GameObject luckyGameResultScreen;

    [SerializeField]
    private Text[] luckyScoreDisplayer;

    private void Start()
    {
        LuckyLastSpawnedObject = null;
        LuckyPlayerHeartsCount = 4;
        luckyGameResultScreen.SetActive(false);
        LuckyGameCurrentScore = 0;
        LuckyGamePlayerTriggeredLevelPiece.AddListener(SpawnLevelPieces);
        LuckyPlayerControllerComponent.luckyFuelEnd.AddListener(LuckyResult);
        SpawnLevelPieces();
    }

    private void SpawnLevelPieces() 
    {
        if (LuckyLastSpawnedObject == null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    LuckyLastSpawnedObject = Instantiate(luckyAsteroidsObjects[Random.Range(0, luckyAsteroidsObjects.Length)], Vector3.zero, Quaternion.identity);
                }
                else
                {
                    LuckyLastSpawnedObject = Instantiate(luckyAsteroidsObjects[Random.Range(0, luckyAsteroidsObjects.Length)], new Vector3(LuckyLastSpawnedObject.transform.position.x, LuckyLastSpawnedObject.transform.position.y, LuckyLastSpawnedObject.transform.position.z + 15), Quaternion.identity);
                }
                
            }
        }
        else
            LuckyLastSpawnedObject = Instantiate(Random.Range(0,100) < 5? luckyPieceWithRing : luckyAsteroidsObjects[Random.Range(0, luckyAsteroidsObjects.Length)], new Vector3(LuckyLastSpawnedObject.transform.position.x, LuckyLastSpawnedObject.transform.position.y, LuckyLastSpawnedObject.transform.position.z + 15), Quaternion.identity);
    }

    private void OnDestroy()
    {
        LuckyGamePlayerTriggeredLevelPiece.RemoveAllListeners();
        LuckyPlayerControllerComponent.luckyFuelEnd.RemoveAllListeners();
    }

    private void LuckyResult() {
        luckyGameResultScreen.SetActive(true);
    }

    private void LateUpdate()
    {
        if (LuckyGameCurrentScore > LuckyPlayerBestScore)
        {
            LuckyPlayerBestScore = LuckyGameCurrentScore;
        }
        foreach (var item in luckyScoreDisplayer)
        {
            item.text = LuckyGameCurrentScore.ToString("0") + " C";
        }
        for (int i = 0; i < luckyPlayerHeartsObjects.Length; i++)
        {
            if (i < LuckyPlayerHeartsCount)
            {
                luckyPlayerHeartsObjects[i].SetActive(true);
            }
            else
            {
                luckyPlayerHeartsObjects[i].SetActive(false);
            }
        }
        if (LuckyPlayerHeartsCount <= 0)
        {
            LuckyResult();
        }
    }

    public void OnClickLuckyLoadMenu() 
    {
        SceneManager.LoadScene("LuckyMenuScen");
    }

    public void OnClickLuckyRestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
