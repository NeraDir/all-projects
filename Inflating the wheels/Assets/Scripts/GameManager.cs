using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int scoreToComplete;
    private int counterWheel;

    [SerializeField] public TextMeshProUGUI textCounter;
    [SerializeField] public TextMeshProUGUI textScore;
    [SerializeField] public List<Image> imagesBeforeCompleteWheel = new List<Image>();
    [SerializeField] public List<Sprite> spritesCompleteWheel = new List<Sprite>();

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        counterWheel = 0;
        scoreToComplete = 10;
        textScore.text = scoreToComplete.ToString();
    }

    public void ChangeState()
    {
        textScore.text = scoreToComplete.ToString();

        if (scoreToComplete == 5)
        {
            for (int i = 0; i < imagesBeforeCompleteWheel.Count; i++)
            {
                imagesBeforeCompleteWheel[i].sprite = spritesCompleteWheel[i];
            }
        }

        if (scoreToComplete == 0)
        {
            counterWheel++;
            scoreToComplete = 10;
            textScore.text = scoreToComplete.ToString();
            textCounter.text = "Колесо № " + counterWheel + " Накаченно!";

            StartCoroutine(MessageText());

            for (int i = 0; i < imagesBeforeCompleteWheel.Count; i++)
            {
                imagesBeforeCompleteWheel[i].sprite = spritesCompleteWheel[i + 2];
            }
        }

        IEnumerator MessageText()
        {
            textCounter.gameObject.SetActive(true);

            yield return new WaitForSeconds(1.1f);

            textCounter.gameObject.SetActive(false);
        }
    }
}