using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText, levelText;
    private int score, level;
    [SerializeField] private CardScript cardPrefab, first, second;
    [SerializeField] private RectTransform grid;
    [SerializeField] private List<CardScript> allCards = new List<CardScript>(), cards = new List<CardScript>();
    [SerializeField] private CanvasGroup victoryCanvas;

    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("Level");
        levelText.text = (level + 1).ToString();
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int i = 0; i < level + 2; i++)
        {
            var c = Instantiate(cardPrefab, grid);
            cards.Add(c);
            allCards.Add(c);
            var c2 = Instantiate(cardPrefab, grid);
            cards.Add(c2);
            allCards.Add(c2);
        }
        for (int i = 0; i < level+2; i++)
        {
            var first = cards[Random.Range(0, cards.Count)];
            first.SetID(i);
            cards.Remove(first);
            var second = cards[Random.Range(0, cards.Count)];
            second.SetID(i);
            cards.Remove(second);
        }
    }

    public void CardClicked (CardScript _card)
    {
        if (first == null)
        {
            first = _card;
            first.Open();
        }
        else
        {
            if (second == null)
            {
                second = _card;
                second.Open();
                Invoke("Comparison", 0.5f);
            }
        }
    }

    private void Comparison()
    {
        if (first.ID == second.ID)
        {
            AddScore();
            allCards.Remove(first);
            allCards.Remove(second);
            first = null;
            second = null;
            if (allCards.Count < 1)
            {
                Victory();
            }
        }
        else
        {
            first.Close();
            second.Close();
            first = null;
            second = null;
        }
    }

    void AddScore()
    {
        score++;
        scoreText.text = string.Format("Score: {0}", score);
    }

    void Victory()
    {
        victoryCanvas.gameObject.SetActive(true);
        victoryCanvas.DOFade(1, 0.5f);
    }

    public void NextLevel()
    {
        level++;
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadScene(2);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }
}
