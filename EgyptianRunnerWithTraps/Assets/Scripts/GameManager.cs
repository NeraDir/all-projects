using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int crystallRocksCount 
    {
        get
        {
            if (PlayerPrefs.HasKey("EgyptianCrystallsRocksValueSaveKey"))
                return PlayerPrefs.GetInt("EgyptianCrystallsRocksValueSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("EgyptianCrystallsRocksValueSaveKey", value);
        }
    }

    public static int egyptianSelectedSkinValue 
    {
        get
        {
            if (PlayerPrefs.HasKey("EgyptianSelectedSkinValueSaveKey"))
                return PlayerPrefs.GetInt("EgyptianSelectedSkinValueSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("EgyptianSelectedSkinValueSaveKey", value);
        }
    }

    private int egyptianCurrentLevelValue 
    {
        get
        {
            if (PlayerPrefs.HasKey("EgyptianCurrentLevelValueSaveKey"))
                return PlayerPrefs.GetInt("EgyptianCurrentLevelValueSaveKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("EgyptianCurrentLevelValueSaveKey", value);
        }
    }

    public static string egyptianGameString;

    public static int egyptianMaxLevelValue 
    {
        get 
        {
            if (PlayerPrefs.HasKey("EgyptianMaxLevelValueSaveKey"))
                return PlayerPrefs.GetInt("EgyptianMaxLevelValueSaveKey");
            return 1;
        }
        set 
        {
            PlayerPrefs.SetInt("EgyptianMaxLevelValueSaveKey", value);
        }
    }

    public static float egyptianTrapsRotationSpeed 
    {
        get
        {
            if (PlayerPrefs.HasKey("egyptianTrapsRotationSpeedSaveKey"))
                return PlayerPrefs.GetFloat("egyptianTrapsRotationSpeedSaveKey");
            return 90;
        }
        set
        {
            PlayerPrefs.SetFloat("egyptianTrapsRotationSpeedSaveKey", value);
        }
    }

    [SerializeField]
    private TMP_Text[] egyptianCurrentLevelShow;

    [SerializeField]
    private TMP_Text[] egyptianCrystallsCountShow;

    [SerializeField]
    private GameObject _egyptianSimpleRoadPiece;

    [SerializeField]
    private GameObject _egyptianFinishRoadPiece;

    private float _egyptianMarginSpawnDistance
    {
        get
        {
            if (PlayerPrefs.HasKey("_egyptianMarginSpawnDistanceSaveKey"))
                return PlayerPrefs.GetFloat("_egyptianMarginSpawnDistanceSaveKey");
            return -13.5f;
        }
        set 
        {
            PlayerPrefs.SetFloat("_egyptianMarginSpawnDistanceSaveKey", value);
        }
    }

    public static bool gameEnded;

    private void Start()
    {
        gameEnded = false;
        _egyptianMarginSpawnDistance = -13.5f;
        SpawnNewLevelRoad();
    }

    private void LateUpdate()
    {
        foreach (var item in egyptianCurrentLevelShow)
        {
            item.text = egyptianCurrentLevelValue.ToString();
        }

        foreach (var item in egyptianCrystallsCountShow)
        {
            item.text = "x" + crystallRocksCount.ToString();
        }
    }

    private void SpawnNewLevelRoad() 
    {
        int countSpawn = 2 + egyptianCurrentLevelValue;
        for (int i = 0; i < countSpawn; i++)
        {
            Instantiate(_egyptianSimpleRoadPiece, new Vector3(0, -5.034597f, _egyptianMarginSpawnDistance), Quaternion.identity);
            _egyptianMarginSpawnDistance += 10.86f;
        }
        Instantiate(_egyptianFinishRoadPiece,new Vector3(0,-5.034597f, _egyptianMarginSpawnDistance),Quaternion.identity);
    }

    public void OnEgyptianRestartButtonPressed()
    {
        if (egyptianMaxLevelValue < egyptianCurrentLevelValue)
        {
            egyptianMaxLevelValue = egyptianCurrentLevelValue;
        }
        _egyptianMarginSpawnDistance = -13.5f;
        egyptianCurrentLevelValue = 1;
        egyptianTrapsRotationSpeed = 90;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationQuit()
    {
        _egyptianMarginSpawnDistance = -13.5f;
        egyptianCurrentLevelValue = 1;
        egyptianTrapsRotationSpeed = 90;
    }

    public void OnEgyptianMenuButtonPressed()
    {
        if (egyptianMaxLevelValue < egyptianCurrentLevelValue)
        {
            egyptianMaxLevelValue = egyptianCurrentLevelValue;
        }
        _egyptianMarginSpawnDistance = -13.5f;
        egyptianCurrentLevelValue = 1;
        egyptianTrapsRotationSpeed = 90;
        SceneManager.LoadScene("Menu");
    }

    public void OnEgyptianNextButtonPressed()
    {
        if (egyptianMaxLevelValue < egyptianCurrentLevelValue)
        {
            egyptianMaxLevelValue = egyptianCurrentLevelValue;
        }
        egyptianCurrentLevelValue++;
        egyptianTrapsRotationSpeed += 10;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
