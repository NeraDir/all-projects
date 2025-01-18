using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GamePlayController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text starsDisplay_TMP;
    [SerializeField]
    private TMP_Text powerNumberDisplay_TMP;



    public static int currentLevelNumber;
    public static int tigerSizePowerValue;
    public static int starsCount;

    public static int lastLevel
    {
        get
        {
            if (!PlayerPrefs.HasKey("LastLeveKey"))
            {
                PlayerPrefs.SetInt("LastLeveKey", 1);
            }
            return PlayerPrefs.GetInt("LastLeveKey");
        }
        set
        {
            PlayerPrefs.SetInt("LastLeveKey", value);
        }
    }

    [SerializeField]
    private GameObject resultUIPanel;
    [SerializeField]
    private GameObject gameOverUIPanel;


    private void OnEnable()
    {
        TigerEntityColliderManager.FinalStoneTriggerEvent += MultiplieStars;
        TigerEntityColliderManager.GameOverEvent += OpenResult;
        TigerEntityHealth.HealthIsOverEvent += OpenGameOver;
        TigerEntityColliderManager.DeadTriggerEvent += OpenGameOver;
    }
    private void OnDisable()
    {
        TigerEntityColliderManager.FinalStoneTriggerEvent -= MultiplieStars;
        TigerEntityColliderManager.GameOverEvent -= OpenResult;
        TigerEntityHealth.HealthIsOverEvent -= OpenGameOver;
        TigerEntityColliderManager.DeadTriggerEvent -= OpenGameOver;
    }

    private void Start()
    {
        currentLevelNumber = lastLevel;
        tigerSizePowerValue = 1;
        starsCount = 0;
    }

    private void Update()
    {
        starsDisplay_TMP.text = (starsCount == 0 ? "0" : starsCount.ToString());
        powerNumberDisplay_TMP.text = tigerSizePowerValue.ToString();
    }

    public void MultiplieStars(int muiltValue)
    {
        tigerSizePowerValue -= 1;
        starsCount *= muiltValue;
    }

    public void OpenResult()
    {
        if(GamePlayData.recordstartdata < starsCount)
        {
            GamePlayData.recordstartdata = starsCount;
        }
        resultUIPanel.SetActive(true);

    }
    public void OpenGameOver()
    {
        gameOverUIPanel.gameObject.SetActive(true);
    }
}
