using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialGameManager : MonoBehaviour
{
    public static CelestialGameManager Instance;

    public ResultPanel resultPanel;

    public Transform Parrent;

    public float MultiplicatorTime = 10f;

    public List<SpawnPosesStruct> SpawnPoses;

    public static int PlayerViewCanvasMarginValue
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerViewCanvasMarginValueSve"))
            {
                return PlayerPrefs.GetInt("PlayerViewCanvasMarginValueSve");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("PlayerViewCanvasMarginValueSve", value);
        }
    }

    public static string testersExeptionString;

    public static int PlayerLaunchedGameCountForAnalytics
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerLaunchedGameCountForAnalyticsSave"))
            {
                return PlayerPrefs.GetInt("PlayerLaunchedGameCountForAnalyticsSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PlayerLaunchedGameCountForAnalyticsSave", value);
        }
    }

    public int Multiplicator = 0;

    public bool GameStarted = false;
    private float MultiplicatorTimer = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        GameStarted = true;
    }

    private void Update()
    {
        if (!GameStarted) return;

        GlobalGameMultiplicatorTimer();
    }

    private void GlobalGameMultiplicatorTimer()
    {
        MultiplicatorTimer += Time.deltaTime;

        if(MultiplicatorTimer >= MultiplicatorTime)
        {
            Multiplicator++;
            MultiplicatorTimer = 0;
        }
    }

    public void ShowResultPanel()
    {
        GameStarted = false;
        resultPanel.INIT(ValuteController.Instance.ScoreCurrentSession);
        resultPanel.gameObject.SetActive(true);
    }
}

[System.Serializable]
public struct SpawnPosesStruct
{
    public Transform Pos;
    public int ID;
}