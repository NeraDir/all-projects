using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class coptercontrolling : MonoBehaviour
{
    public GameObject[] hearts;

    private int heartsCount;

    private int score;

    private Rigidbody copterBody;

    public TMP_Text[] scoreTXT;

    public TMP_Text exempleTxt;

    public TMP_Text exempleTXTPREfab;

    private int trueAnswer;

    [SerializeField]
    private Vector2[] spawningPositions;

    private List<exempleAnswerComponent> temptxts = new List<exempleAnswerComponent>();

    public GameObject resulPage;

    public static float speedOfAnswers;

    private void Start()
    {
        heartsCount = 3;
        speedOfAnswers = 0.75f;
        Time.timeScale = 1;
        copterBody = GetComponent<Rigidbody>();
        SetNewQuetion();
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            copterBody.AddForce(Vector2.up * 3, ForceMode.Impulse);
        }

        foreach (var item in scoreTXT)
        {
            item.text = score.ToString();
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < heartsCount)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }

        if (score > coptersaves.eliteBestScore)
        {
            coptersaves.eliteBestScore = score;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out exempleAnswerComponent answer))
        {
            if (answer.answerValue == trueAnswer)
            {
                score += 5;
                foreach (var item in temptxts)
                {
                    Destroy(item.gameObject);
                }
                temptxts.Clear();
                SetNewQuetion();
            }
            else
            {
                heartsCount -= 1;
                if (heartsCount > 0)
                {
                    foreach (var item in temptxts)
                    {
                        Destroy(item.gameObject);
                    }
                    temptxts.Clear();
                    SetNewQuetion();
                }
                else
                {
                    resulPage.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }

        if (other.TryGetComponent(out endingWall wall))
        {
            resulPage.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void SetNewQuetion() 
    {
        int firstvalue = Random.Range(10, 200);
        int secondValue = Random.Range(10, 200);
        trueAnswer = firstvalue + secondValue;
        speedOfAnswers += 0.25f;
        exempleTxt.text = firstvalue.ToString() + "+" + secondValue.ToString();
        foreach (var item in spawningPositions)
        {
            exempleAnswerComponent ansewet = Instantiate(exempleTXTPREfab.GetComponent<exempleAnswerComponent>(), item, Quaternion.identity);
            ansewet.answerValue = Random.Range(trueAnswer, trueAnswer + 10);
            ansewet.Show();
            temptxts.Add(ansewet);
        }
        int rnd = Random.Range(0, temptxts.Count);
        temptxts[rnd].answerValue = trueAnswer;
        temptxts[rnd].Show();
    }

    public void Menu() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("loading");
        
    }

    public void Restart() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
