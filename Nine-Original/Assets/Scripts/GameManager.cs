using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Lose WinPanel;
    [SerializeField] private Lose LosePanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public static int Points { get; set; }
    public static bool GameStarted { get; set; }

    [SerializeField] private TMP_Text PointsTXT;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        SetPoints(0);
        GameStarted = true;

        Field.Instance.CreateField();
    }

    public void Win()
    {
        GameStarted = false;

        SaveClass.MaxScore = Points;
        WinPanel.SetScore(Points);
        WinPanel.gameObject.SetActive(true);
    }

    public void Lose()
    {
        GameStarted = false;

        SaveClass.MaxScore = Points;
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
