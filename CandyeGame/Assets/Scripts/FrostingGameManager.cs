using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FrostingGameManager : MonoBehaviour
{
    public static float candysSpawnTime;

    public static float candysMoveSpeed;

    public static Sprite needCandySprite;

    public static int candysHeartsCount 
    {
        get
        {
            if (PlayerPrefs.HasKey("FrostingCandysHearts"))
            {
                return PlayerPrefs.GetInt("FrostingCandysHearts");
            }
            return 3;
        }
        set 
        {
            PlayerPrefs.SetInt("FrostingCandysHearts", value);
        }
    }

    public static int frostingCandysLevelIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("frostingCandysLevelIndex"))
            {
                return PlayerPrefs.GetInt("frostingCandysLevelIndex");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("frostingCandysLevelIndex", value);
        }
    }

    public static int frostingCurrentTempLevel 
    {
        get
        {
            if (PlayerPrefs.HasKey("frostingCurrentTempLevel"))
            {
                return PlayerPrefs.GetInt("frostingCurrentTempLevel");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("frostingCurrentTempLevel", value);
        }
    }

    public static string frostingDefaultLevelKey;

    public static int frostingCandysBeginSpeed
    {
        get
        {
            if (PlayerPrefs.HasKey("frostingCandysBeginSpeed"))
            {
                return PlayerPrefs.GetInt("frostingCandysBeginSpeed");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("frostingCandysBeginSpeed", value);
        }
    }

    [SerializeField]
    private GameObject[] levels;

    [SerializeField]
    private Image needImage;

    [SerializeField]
    private TMP_Text showNeedCount;

    [SerializeField]
    private TMP_Text showcandyresult;

    [SerializeField]
    private Image[] candyHearts;

    [SerializeField]
    private Sprite[] heartSprites;

    [SerializeField]
    private Sprite[] candysPool;

    [SerializeField]
    private GameObject candyNextButton;

    [SerializeField]
    private GameObject candyResultScreen;

    public static float needCount;

    public static bool candyGameStarted;

    public static float currentCount;

    private void Start()
    {
        switch (frostingDefaultLevelKey)
        {
            case "easy":
                candysMoveSpeed = 4;
                candysSpawnTime = 2;
                break;
            case "normal":
                candysMoveSpeed = 2;
                candysSpawnTime = 1.5f;
                break;
            case "hard":
                candysMoveSpeed = 1f;
                candysSpawnTime = 1;
                break;
        }

        needCandySprite = candysPool[Random.Range(0, candysPool.Length)];
        needCount = Random.Range(5, 10);
        foreach (var item in levels)
        {
            if (item.name.ToLower().Contains(frostingDefaultLevelKey))
            {
                item.SetActive(true);
            }
        }
        needImage.sprite = needCandySprite;
        currentCount = 0;
        candyGameStarted = true;
    }

    private void LateUpdate()
    {
        if (!candyGameStarted)
            return;
        Debug.Log(currentCount);
        for (int i = 0; i < candyHearts.Length; i++)
        {
            if (i >= candysHeartsCount)
            {
                candyHearts[i].sprite = heartSprites[1];
            }
            else
            {
                candyHearts[i].sprite = heartSprites[0];
            }
        }
        showNeedCount.text = "x" + needCount.ToString("0");
        if (candysHeartsCount <= 0 && currentCount < needCount)
        {
            showcandyresult.text = "LEVEL LOOSE";
            candyNextButton.SetActive(false);
            candyResultScreen.SetActive(true);
            candyGameStarted = false;
        }
        if (currentCount >= needCount && candysHeartsCount > 0)
        {
            candyGameStarted = false;
            showcandyresult.text = "LEVEL COMPLETE";
            candyNextButton.SetActive(true);
            candyResultScreen.SetActive(true);
        }
    }

    private void OnApplicationQuit()
    {
        frostingCurrentTempLevel = 1;
        candysHeartsCount = 3;
    }

    public void OnClickOpenMenu() 
    {
        frostingCurrentTempLevel = 1;
        candysHeartsCount = 3;
        SceneManager.LoadScene("FrostingMenu");
    }

    public void OnClickRestart() 
    {
        frostingCurrentTempLevel = 1;
        candysHeartsCount = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickNext()
    {
        frostingCurrentTempLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
