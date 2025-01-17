using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragonLanGameController : MonoBehaviour
{
    [SerializeField]
    private Transform dragonTransform;

    [SerializeField]
    private GameObject dragonResultScreen;

    [SerializeField]
    private GameObject dragonResultNextButton;

    [SerializeField]
    private Text dragonResultTxt;

    [SerializeField]
    private Text[] showCurrentCoinsBag;

    [SerializeField]
    private Text[] showCurrentLevel;

    [SerializeField]
    private Text showHardLevel;

    [SerializeField]
    private Text showFirebalsCount;

    [SerializeField]
    private Image fireballBar;

    [SerializeField]
    private GameObject[] typesPrefabs;

    [SerializeField]
    private GameObject gatePrefab;

    private GameObject lastSpawnedType;

    public static Transform DragonLanTransform;

    public static int coinsPerLevel;

    public static float fireballs;

    public static int endX;


    public static int coins 
    {
        get
        {
            if (PlayerPrefs.HasKey("DragonLanMainCoinsSaveKey"))
                return PlayerPrefs.GetInt("DragonLanMainCoinsSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("DragonLanMainCoinsSaveKey", value);
        }
    }

    public static int currentLevel 
    {
        get
        {
            if (PlayerPrefs.HasKey("DragonLanCurrentLevelSaveKey"))
                return PlayerPrefs.GetInt("DragonLanCurrentLevelSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("DragonLanCurrentLevelSaveKey", value);
        }
    }

    public static int MaxLevel 
    {
        get
        {
            if (PlayerPrefs.HasKey("DragonLanMaxLevelSaveKey"))
                return PlayerPrefs.GetInt("DragonLanMaxLevelSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("DragonLanMaxLevelSaveKey", value);
        }
    }

    public static bool dragonAlive;

    private float maxFireballs;

    private void Awake()
    {
        if (DragonLanGameController.currentLevel != 0 && currentLevel % 3 == 0)
        {
            showHardLevel.text = "HARD";
        }
        else
        {
            showHardLevel.text = "";
        }
        endX = 1;
        DragonLanObjectFollower.move = false;
        fireballs = 0;
        lastSpawnedType = null;
        coinsPerLevel = 0;
        DragonLanTransform = dragonTransform;
        dragonAlive = true;
        SpawnLevel();
    }

    private void LateUpdate()
    {
        foreach (var item in showCurrentCoinsBag)
        {
            item.text = (coinsPerLevel * endX).ToString("0");
        }

        foreach (var item in showCurrentLevel)
        {
            item.text = "LVL:"+ (currentLevel + 1).ToString("0");
        }

        showFirebalsCount.text = "FIREBALLS: " + fireballs.ToString("0");

        if (currentLevel > MaxLevel)
        {
            MaxLevel = currentLevel;
        }

        fireballBar.fillAmount = Mathf.Lerp(fireballBar.fillAmount, fireballs/maxFireballs,8 * Time.deltaTime);
        if (dragonAlive)
        {
            if (FindObjectOfType<DragonLanGate>() == null)
            {
                dragonResultScreen.SetActive(true);
                dragonResultTxt.text = "YOU WIN!";
                dragonResultNextButton.SetActive(true);
                DragonLanObjectFollower.move = true;
            }
            else
            {
                DragonLanObjectFollower.move = false;
            }
        }
        else if (!dragonAlive)
        {
            dragonResultScreen.SetActive(true);
            dragonResultNextButton.SetActive(false);
            dragonResultTxt.text = "YOU LOOSE";
            DragonLanObjectFollower.move = true;
        }
    }

    private void SpawnLevel() 
    {
        for (int i = 0; i < currentLevel + 2; i++) 
        {
            if (lastSpawnedType == null)
            {
                lastSpawnedType = Instantiate(typesPrefabs[0], new Vector3(0, -1.393f, 0), Quaternion.identity);
            }
            else
            {
                lastSpawnedType = Instantiate(typesPrefabs[Random.Range(0, typesPrefabs.Length)], new Vector3(0, -1.393f, lastSpawnedType.transform.position.z + 4.83f), Quaternion.identity);
            }
        }
        if (FindObjectOfType<DragonLanGetBullet>() == null)
        {
            GameObject tempObject =  Instantiate(typesPrefabs[1], lastSpawnedType.transform.position, lastSpawnedType.transform.rotation);
            Destroy(lastSpawnedType);
            lastSpawnedType = tempObject;
        }
        for (int i = 0; i < FindObjectsOfType<DragonLanGetBullet>().Length; i++)
        {
            maxFireballs += 1;
            lastSpawnedType = Instantiate(gatePrefab, new Vector3(0, -1.45f, lastSpawnedType.transform.position.z + 8.26f), Quaternion.Euler(0, 90, 0));
            if (currentLevel % 4 == 0)
            {
                lastSpawnedType.GetComponent<DragonLanGate>().x = i + 6;
            }
            else
            {
                lastSpawnedType.GetComponent<DragonLanGate>().x = i + 2;
            }
        }
    }

    private void OnApplicationQuit()
    {
        currentLevel = 0;
    }

    public void OnClickNext() 
    {
        currentLevel++;
        coins += coinsPerLevel * endX;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart() 
    {
        currentLevel = 0;
        coins += coinsPerLevel * endX;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu() 
    {
        currentLevel = 0;
        coins += coinsPerLevel * endX;
        SceneManager.LoadScene("DragonLanMenuScene");
    }
}
