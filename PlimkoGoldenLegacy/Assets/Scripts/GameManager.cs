using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int plimkoBallsCountPerLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("plimkoBallsCountPerLevelSave"))
            {
                return PlayerPrefs.GetInt("plimkoBallsCountPerLevelSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("plimkoBallsCountPerLevelSave", value);
        }
    }

    public static string plimkoMainSceneName;

    public static int plimkoLevelsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("plimkoLevelsCountSave"))
            {
                return PlayerPrefs.GetInt("plimkoLevelsCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("plimkoLevelsCountSave", value);
        }
    }

    [SerializeField]
    private GameObject ball;

    public static int bestScore 
    {
        get 
        {
            if (PlayerPrefs.HasKey("plimkoBesterScoreer"))
            {
                return PlayerPrefs.GetInt("plimkoBesterScoreer");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("plimkoBesterScoreer", value);
        }
    }

    public static int selectedPlatform
    {
        get
        {
            if (PlayerPrefs.HasKey("plimkoselectedPlatformSave"))
            {
                return PlayerPrefs.GetInt("plimkoselectedPlatformSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("plimkoselectedPlatformSave", value);
        }
    }

    public static int selectedBall
    {
        get
        {
            if (PlayerPrefs.HasKey("plimkoselectedBallSave"))
            {
                return PlayerPrefs.GetInt("plimkoselectedBallSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("plimkoselectedBallSave", value);
        }
    }

    public static int coins
    {
        get
        {
            if (PlayerPrefs.HasKey("plimkcoins"))
            {
                return PlayerPrefs.GetInt("plimkcoins");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("plimkcoins", value);
        }
    }

    [SerializeField]
    private TMP_Text[] currentScoreShower;

    public static int score;

    public static List<GameObject> ballsAlive = new List<GameObject>();

    public GameObject endPage;

    public GameObject won;

    public GameObject loose;

    [SerializeField]
    private TMP_Text needshower;

    [SerializeField]
    private TMP_Text coinsTXT;

    private int coinsPerLevel;

    [SerializeField]
    private Sprite[] _ballsSprites;

    [SerializeField]
    private Sprite[] _platformSprites;

    [SerializeField]
    private SpriteRenderer ballSpriteRenderer;

    [SerializeField]
    private SpriteRenderer[] platformSpriteRenderer;

    private int otherLevelNeedValue 
    {
        get
        {
            if (PlayerPrefs.HasKey("plimkoCountotherLevelNeedValue"))
            {
                return PlayerPrefs.GetInt("plimkoCountotherLevelNeedValue");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("plimkoCountotherLevelNeedValue", value);
        }
    }

    private int otherlevel 
    {
        get 
        {
            if (PlayerPrefs.HasKey("plimkoCountToOtherLevel"))
            {
                return PlayerPrefs.GetInt("plimkoCountToOtherLevel");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("plimkoCountToOtherLevel", value);
        }
    }

    private int beginerValue 
    {
        get 
        {
            if (PlayerPrefs.HasKey("beginerval"))
            {
                return PlayerPrefs.GetInt("beginerval");
            }
            return 3;
        }
        set 
        {
            PlayerPrefs.SetInt("beginerval", value);
        }
    }

    public static int level 
    {
        get 
        {
            if (PlayerPrefs.HasKey("tempLevlvalue"))
            {
                return PlayerPrefs.GetInt("tempLevlvalue");
            }
            return 1;
        }
        set 
        {
            PlayerPrefs.SetInt("tempLevlvalue", value);
        }
    }

    public int needValue;

    public TMP_Text showLvl;

    private void Start()
    {
        if (otherLevelNeedValue == 0)
        {
            otherLevelNeedValue = Random.Range(1, 4);
        }
        foreach (var item in platformSpriteRenderer)
        {
            item.sprite = _platformSprites[selectedPlatform];
        }
        ballSpriteRenderer.sprite = _ballsSprites[selectedBall];
        needValue = 0;
        needValue = beginerValue * level;
        score = 0;
        for (int i = 0; i < 2 * level; i++)
        {

            ballsAlive.Add(Instantiate(ball, new Vector3(Random.Range(-2.33f, 2.33f), 3.9f, 0), Quaternion.identity));
        }
        StartCoroutine(CheckBalls());
    }

    private IEnumerator CheckBalls() 
    {
        while (ballsAlive.Count != 0)
        {
            yield return null;
        }
        coinsPerLevel = Random.Range(10, 20);
        if (needValue <= score)
        {
            won.SetActive(true);
        }
        else
        {
            loose.SetActive(true);
        }
    }

    private void Update()
    {
        if (score > bestScore)
        {
            bestScore = score;
        }
        showLvl.text = "lvl " + level.ToString("0");
        needshower.text = "x" + needValue.ToString("0");
        foreach (var item in currentScoreShower)
        {
            item.text = "x" + score.ToString("0");
        }
        coinsTXT.text = coinsPerLevel.ToString();
    }

    public void ClickNext()
    {
        otherlevel += 1;
        if (otherlevel >= otherLevelNeedValue)
        {
            level++;
            beginerValue += 2;
            coins += coinsPerLevel;
            SceneManager.LoadScene("games2");
            otherlevel = 0;
            otherLevelNeedValue = 0;
        }
        else
        {
            level++;
            beginerValue += 2;
            coins += coinsPerLevel;
            SceneManager.LoadScene("games");
        }
        
    }

    public void ClickRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ClickMenu() 
    {
        otherlevel = 0;
        otherLevelNeedValue = 0;
        level = 1;
        beginerValue = 3;
        coins += coinsPerLevel;
        SceneManager.LoadScene("menus");
    }

    private void OnApplicationQuit()
    {
        otherlevel = 0;
        otherLevelNeedValue = 0;
        level = 1;
        beginerValue = 3;
    }
}
