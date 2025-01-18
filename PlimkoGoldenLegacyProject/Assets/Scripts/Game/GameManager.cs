using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<TrolleyType> trolleyTypes = new();

    [SerializeField]
    private TMP_Text[] currentScoreTXT;

    public int currentScore = 0;

    [SerializeField]
    private Button menuButton;

    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private Image[] heartsImages;

    public int heartsCount;

    [SerializeField]
    private Image resultScreen;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        menuButton.onClick.AddListener(OnMenuButtonPressed);
        restartButton.onClick.AddListener(OnRestartButtonPressed);
    }

    private void LateUpdate()
    {
        foreach (var txt in currentScoreTXT)
        {
            txt.text = currentScore.ToString();
        }

        for (int i = 0; i < heartsImages.Length; i++)
        {
            if (i < heartsCount)
            {
                heartsImages[i].gameObject.SetActive(true);
            }
            else
            {
                heartsImages[i].gameObject.SetActive(false);
            }
        }

        if (heartsCount <= 0)
        {
            resultScreen.gameObject.SetActive(true);
        }
    }

    private void OnMenuButtonPressed() 
    {
        if (currentScore > GameDataSaves.PlayerBestScoreValue)
        {
            GameDataSaves.PlayerBestScoreValue = currentScore;
        }
        SceneManager.LoadScene(sceneName);
    }

    private void OnRestartButtonPressed() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

[System.Serializable]
public struct TrolleyType
{
    public ColorVariant Color;
    public Sprite sprite;
}