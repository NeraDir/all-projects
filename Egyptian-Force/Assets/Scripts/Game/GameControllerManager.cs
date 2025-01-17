using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControllerManager : MonoBehaviour
{
    public static GameControllerManager Instance;
    [SerializeField] private LosePanel WinPanel;
    [SerializeField] private LosePanel LosePanel;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public static int Points { get; set; }
    public static bool GameStarted { get; set; }

    [SerializeField] private TMP_Text PointsTXT;

    private void Start()
    {
        StartGame();
    }

    public void StartGame() // DOTween
    {
        SetPoints(0);
        GameStarted = true;

        FieldScript.Instance.GenerateField();
    }

    public void Win()
    {
        GameStarted = false;

        GlobalSave.MaxScore = Points;
        WinPanel.SetScore(Points);
        WinPanel.gameObject.SetActive(true);
    }

    public void Lose()
    {
        GameStarted = false;

        GlobalSave.MaxScore = Points;
        LosePanel.SetScore(Points);
        LosePanel.gameObject.SetActive(true);
    }

    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    private void SetPoints(int points)
    {
        Points = points;
        PointsTXT.text = $"{Points}";
    }
}
