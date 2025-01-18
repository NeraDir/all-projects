using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamecontrollercomponent : MonoBehaviour
{
    public static int maxstarsreach
    {
        get
        {
            if (PlayerPrefs.HasKey("maxstarsreachsavekeybull"))
                return PlayerPrefs.GetInt("maxstarsreachsavekeybull");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("maxstarsreachsavekeybull", value);
        }
    }

    public static int gamecontrollerbullstartspeedvalue
    {
        get
        {
            if (PlayerPrefs.HasKey("gamecontrollerbullstartspeedvaluesavekey"))
            {
                return PlayerPrefs.GetInt("gamecontrollerbullstartspeedvaluesavekey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("gamecontrollerbullstartspeedvaluesavekey", value);
        }
    }

    public static string gamecontrollergamedatasettingkey;

    public static float gamemaxlifetimereached
    {
        get
        {
            if (PlayerPrefs.HasKey("gamemaxlifetimereachedsavekey"))
            {
                return PlayerPrefs.GetFloat("gamemaxlifetimereachedsavekey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("gamemaxlifetimereachedsavekey", value);
        }
    }

    public static int gamelaunchcountdatavalue
    {
        get
        {
            if (PlayerPrefs.HasKey("gamelaunchcountdatavaluesavekey"))
            {
                return PlayerPrefs.GetInt("gamelaunchcountdatavaluesavekey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("gamelaunchcountdatavaluesavekey", value);
        }
    }

    [SerializeField]
    private GameObject[] _platforms;

    [SerializeField]
    private Text[] _lifeTimeTxt;

    [SerializeField]
    private Text[] _starsCountTxt;

    [SerializeField]
    private GameObject _resultPanel;

    [SerializeField]
    private Image _currentCrystallImage;

    [SerializeField]
    private crystallspresetconfigcomponent _crystallspresetconfigcomponent;

    private int currentStars = 0;
    private float currentLifeTime = 0;

    public static crystallParam currentCrystall;

    public static bool canSpawnNew;

    private void Awake()
    {
        canSpawnNew = false;
        currentLifeTime = 60f;
        Time.timeScale = 1;
        tubecontroller.spawnedCrystall.AddListener(OnSpawnedCrystall);
        crystallcomponents.starGetted.AddListener(OnBullStarGet);
        OnSpawnedCrystall();
    }

    private void OnSpawnedCrystall()
    {
        if (canSpawnNew)
            return;
        canSpawnNew = true;
        currentCrystall = _crystallspresetconfigcomponent.crystallParams[Random.Range(0, _crystallspresetconfigcomponent.crystallParams.Count)];
        _currentCrystallImage.sprite = currentCrystall.Sprite;
        Invoke(nameof(caner), 1);
    }

    private void caner() 
    {
        canSpawnNew = false;
    }

    private void LateUpdate()
    {
        if (currentStars > maxstarsreach)
        {
            maxstarsreach = currentStars;
        }
        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime < 0 )
        {
            OnBullDeath();
        }
        foreach (var item in _lifeTimeTxt)
        {
            item.text = currentLifeTime.ToString("0") + "s";
        }
    }

    private void OnBullDeath()
    {
        Time.timeScale = 0;
        _resultPanel.SetActive(true);
    }

    private void OnBullStarGet(int value)
    {
        currentStars += value;
        foreach (var item in _starsCountTxt)
        {
            item.text = currentStars.ToString("0") +"b";
        }
    }

    private void OnBullReachedThPlatform()
    {

    }

    private void OnDestroy()
    {
        
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("gamescene");
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("menuscene");
    }
}
