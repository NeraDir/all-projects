using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _endGameScoreText;

    public void AddScore(int amount)
    {
        score += amount;
        Display();
    }

    private void Start()
    {
        score = 0;
    }

    public void DicreaseScore(int amount) 
    {
        score -= amount;
        Display();
    }

    private void Display()
    {
        _scoreText.text = score.ToString("00000");
        _endGameScoreText.text = score.ToString("00000");
    }
}
