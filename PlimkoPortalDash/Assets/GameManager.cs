using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int bootstrapSettingsWidth
    {
        get
        {
            if (PlayerPrefs.HasKey("bootstrapSettingsWidthSave"))
            {
                return PlayerPrefs.GetInt("bootstrapSettingsWidthSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("bootstrapSettingsWidthSave", value);
        }
    }

    public static string bootstrapKey;

    public static int bootstrapSettingsInitedFirstTime
    {
        get
        {
            if (PlayerPrefs.HasKey("bootstrapSettingsInitedFirstTimeSave"))
            {
                return PlayerPrefs.GetInt("bootstrapSettingsInitedFirstTimeSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("bootstrapSettingsInitedFirstTimeSave", value);
        }
    }

    //public ResultPanel resultPanel;

    public Transform Parrent;
    public bool GameStarted = false;
    public Transform ScreenUp;
    public Transform ScreenDown;
    public Transform BoxParrent;
    public Transform BoxCenter;

    public Transform MaxLeftEnemySpawnPos;
    public Transform MaxRightEnemySpawnPos;

    public GameObject PlayersBallPrefab;

    public TMP_Text PriceNewBallTXT;

    private int PriceNewBall = 120;

    [HideInInspector] public int GameStadia = 0;

    private float TimeToStaiaChange = 40f;
    private float TimerToChangeStadia = 0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //Invoke(nameof(StartGame), 2f);

        AddPlayersBallToBox();
        AddPlayersBallToBox();
        AddPlayersBallToBox();
    }

    private void Update()
    {
        if (!GameStarted) return;

        if (GameStadia < 2)
        {
            TimerToChangeStadia += Time.deltaTime;

            if(TimerToChangeStadia >= TimeToStaiaChange)
            {
                GameStadia++;
                TimerToChangeStadia = 0f;
            }
        }
    }

    public void StartGame()
    {
        PriceNewBallTXT.text = PriceNewBall.ToString();
        GameStarted = true;
    }

    public void BuyNewBall()
    {
       // if (ValutesController.Instance.Gold < PriceNewBall) return;

       // ValutesController.Instance.AddGold(-PriceNewBall);
        PriceNewBall += 130;
        PriceNewBallTXT.text = PriceNewBall.ToString();

        AddPlayersBallToBox();
    }

    public void GoMenu()
    {
        ShowResultPanel();
    }

    public void AddPlayersBallToBox()
    {
        Instantiate(PlayersBallPrefab, BoxCenter.position, Quaternion.identity, BoxParrent);
    }

    public void ShowResultPanel()
    {
        GameStarted = false;

       // resultPanel.gameObject.SetActive(true);
        //resultPanel.INIT(ValutesController.Instance.ScoreNonSave);
    }
}
