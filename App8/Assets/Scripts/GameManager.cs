using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int LevelCount = 0;

    public static int CountHelpPazzle
    {
        get
        {
            if (!PlayerPrefs.HasKey("CountHelpPazzleSave"))
                return 3;

            return PlayerPrefs.GetInt("CountHelpPazzleSave");
        }
        set
        {
            PlayerPrefs.SetInt("CountHelpPazzleSave", value);
        }
    }

    public static int BestTimeSeconds
    {
        get
        {
            if (!PlayerPrefs.HasKey("BestTimeSecondsSave"))
                return 0;

            return PlayerPrefs.GetInt("BestTimeSecondsSave");
        }
        set
        {
            PlayerPrefs.SetInt("BestTimeSecondsSave", value);
        }
    }

    public TMP_Text LevelTXT;
    public TMP_Text HelpPazzleTXT;
    public TMP_Text GoodPiecesTXT;
    public TMP_Text TimerTXT;

    public int CountAllPieces = 0;
    public int CountGoodPieces = 0;
    public int GameTimer = 0;

    private float Timer = 0;
    private float TimeToPlusGameTimer = 1f;

    public GameObject FullPazzlePanel;

    [Header("Field")]
    public Transform FieldSpawnPos;
    public Transform Parrent;

    public List<FieldController> FieldsList = new();

    public Image FullImagePazzleIMG;
    public WinPanelController winPanel;

    public AudioSource MusicSource;
    public AudioSource SoundsSource;

    public AudioClip WinSound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        GoodPiecesTXT.text = $"Filled Pieces {CountGoodPieces}/{CountAllPieces}";
        TimerTXT.text = $"{GameTimer} Seconds";
        HelpPazzleTXT.text = CountHelpPazzle.ToString();

        LevelTXT.text = "LEVEL " + LevelCount.ToString();

        SpawnField();

        if (SettingsController.SoundsToggle == 0)
            MusicSource.Play();
    }

    public void PlayWinSound()
    {
        if (SettingsController.SoundsToggle == 0)
            SoundsSource.PlayOneShot(WinSound);
    }

    public void ShowFullPazzleBTN()
    {
        if (CountHelpPazzle == 0) return;
        CountHelpPazzle--;
        HelpPazzleTXT.text = CountHelpPazzle.ToString();

        FullPazzlePanel.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

    public void SpawnField()
    {
        int rndField = Random.Range(0, FieldsList.Count);
        FieldController buff = Instantiate(FieldsList[rndField], FieldSpawnPos.position, Quaternion.identity, Parrent);
    }

    public void SetFullImage(Sprite sprite)
    {
        FullImagePazzleIMG.sprite = sprite;
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= TimeToPlusGameTimer)
        {
            GameTimer++;
            TimerTXT.text = $"{GameTimer} Seconds";
            Timer = 0f;
        }
    }

    public void AddGoodPiece()
    {
        CountGoodPieces++;
        GoodPiecesTXT.text = $"Filled Pieces {CountGoodPieces}/{CountAllPieces}";

        if (CountGoodPieces >= CountAllPieces)
        {
            ShowWinWindow();
        }
    }

    public void ShowWinWindow()
    {
        PlayWinSound();
        winPanel.Init();
        winPanel.gameObject.SetActive(true);
    }
}