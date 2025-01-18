using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public gameManagerTemper gameManager;
    public TextMeshProUGUI scoreText;
    public ParticleSystem fireworks;

    public GameObject endScreen;

    public GameObject nextButton;

    public TMP_Text resultTxt;

    private int targetScore;

    [SerializeField]
    Animator CarAnim;

    public static int score;
    private bool endingGame;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManagerTemper.levelIndex +1 >= 6)
        {
            for (int i = 5; i < gameManagerTemper.levelIndex + 1; i++)
            {
                targetScore += 100;
            }
        }
        else
        {
            for (int i = 0; i < gameManagerTemper.levelIndex + 1; i++)
            {
                targetScore += 50;
            }
        }
        
        gameManager = Object.FindObjectOfType<gameManagerTemper>(); 
        score = 1;
        endingGame = false;
    }

    private void LateUpdate()
    {
        scoreText.text = $"{score}/{targetScore}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Balls ball))
        {
            score += Random.Range(1,2);
            ball.gameManager.activeBalls.Remove(ball);
            Destroy(ball.gameObject);
            if (!fireworks.isPlaying)
            {
                fireworks.Play();
            }

            if (!endingGame)
            {
                StartCoroutine(Finish());
            }
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSecondsRealtime(5);
        endingGame = true;
        CarAnim.enabled = true;
        yield return new WaitForSeconds(1);
        if (score > targetScore)
        {
            nextButton.SetActive(true);
            if (gameManagerTemper.levelIndex+1 == 10)
            {
                nextButton.SetActive(false);
            }
            
            resultTxt.text = "LEVEL COMPLETED";
        }
        else
        {
            nextButton.SetActive(false);
            resultTxt.text = "LEVEL NOT COMPLETED";
        }
        endScreen.SetActive(true);
    }
}
