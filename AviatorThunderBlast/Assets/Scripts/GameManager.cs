using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] thunderWallsPrefabs;

    public Transform thunderWallsSpawnPosition;

    public GameObject thunderGameResultWindow;

    public Transform thunderStarsoveTo;

    public Text[] showStarsCount;

    public Text[] showDistance;

    public Transform planeTransform;

    public GameObject[] planeSkins;

    private Vector3 thunderStartPosition;

    private float thunderCurrentDistance;

    public static int thunderMaxStarsEarnedCount
    {
        get
        {
            if (PlayerPrefs.HasKey("ThunderMaxStarsEarnedCountSave"))
            {
                return PlayerPrefs.GetInt("ThunderMaxStarsEarnedCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ThunderMaxStarsEarnedCountSave", value);
        }
    }


    public static int thunderGameBeganWallsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("thunderGameBeganWallsCountSave"))
            {
                return PlayerPrefs.GetInt("thunderGameBeganWallsCountSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("thunderGameBeganWallsCountSave", value);
        }
    }

    public static string thunderLevelName;

    public static int thunderPlaneSelectedIndex 
    {
        get
        {
            if (PlayerPrefs.HasKey("ThunderPlaneSelectedIndexSave"))
            {
                return PlayerPrefs.GetInt("ThunderPlaneSelectedIndexSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ThunderPlaneSelectedIndexSave", value);
        }
    }

    public static Transform starMoveToPosition;

    public static float thunderBestDistanceReached 
    {
        get
        {
            if (PlayerPrefs.HasKey("ThunderBestDistanceReachedSave"))
            {
                return PlayerPrefs.GetFloat("ThunderBestDistanceReachedSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("ThunderBestDistanceReachedSave", value);
        }
    }

    public static int thunderBeganPositionZValue
    {
        get
        {
            if (PlayerPrefs.HasKey("thunderBeganPositionZValueSave"))
            {
                return PlayerPrefs.GetInt("thunderBeganPositionZValueSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("thunderBeganPositionZValueSave", value);
        }
    }

    public static float moveSpeed;

    private float timeSpawn;

    public static bool isEnd;

    private GameObject currentWall;

    private void Awake()
    {
        isEnd = false;
        thunderCurrentDistance = 0;
        moveSpeed = 10;
        timeSpawn = 4;
        planeSkins[thunderPlaneSelectedIndex].SetActive(true);
        starMoveToPosition = thunderStarsoveTo;
        StartCoroutine(SpawnThunderWalls());
    }

    private void LateUpdate()
    {
        if (isEnd) 
        {
            thunderGameResultWindow.SetActive(true);
            return;
        }
        thunderCurrentDistance = Vector3.Distance(thunderStartPosition, planeTransform.position);
        foreach (var item in showDistance)
        {
            item.text = thunderCurrentDistance.ToString("0.00") + "m";
        }

        foreach (var item in showStarsCount)
        {
            item.text = "X" + thunderMaxStarsEarnedCount.ToString("0");
        }

        if (thunderCurrentDistance > thunderBestDistanceReached)
        {
            thunderBestDistanceReached = thunderCurrentDistance;
        }
    }

    public void OnButtonPressed(string input) 
    {
        switch (input)
        {
            case "Restart":
                OnRestart();
                break;
            case "Menu":
                OnMenu();
                break;
        }
    }

    private void OnRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnMenu() 
    {
        SceneManager.LoadScene("ThunderMenuScene");
    }

    private IEnumerator SpawnThunderWalls() 
    {
        while (true) 
        {
            if (currentWall != null)
            {
                if (currentWall.transform.position.z <= thunderWallsSpawnPosition.position.z - 70)
                {
                    currentWall = Instantiate(thunderWallsPrefabs[Random.Range(0, thunderWallsPrefabs.Length)], thunderWallsSpawnPosition.position, thunderWallsSpawnPosition.rotation);
                    moveSpeed += 0.1f;
                }
            }
            else
            {
                currentWall = Instantiate(thunderWallsPrefabs[Random.Range(0, thunderWallsPrefabs.Length)], thunderWallsSpawnPosition.position, thunderWallsSpawnPosition.rotation);
            }
            yield return null;
        }
    }
}
