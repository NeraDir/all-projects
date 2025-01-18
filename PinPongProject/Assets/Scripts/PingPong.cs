using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PingPong : MonoBehaviour
{
    public WinControll WinPanel;
    public GameObject losePanel;
    public TMP_Text RemainingTXT;
    public float TimeToLose = 6f;
    public Slider SliderTimer;

    [Space(5)]

    public Transform player;
    public Transform ball;
    public int startBallSpeed = 350;
    public float playerSpeed = 10;
    public float computerSpeed = 2.5f;
    public float playerLimitY = 3.5f;
    public int playerScore;

    private float Timer = 0f;
    private int RemainedMax = 0;

    private bool GameActive = false;

    private void Awake()
    {
        Physics2D.gravity = new Vector2(-9.81f, 0f);
    }

    void Start()
    {
        SliderTimer.maxValue = TimeToLose;
        SliderTimer.value = TimeToLose;
        RemainedMax = playerScore;
        RemainingTXT.text = playerScore.ToString() + " / " + RemainedMax.ToString();
        Reset(0);
    }

    private void Update()
    {
        if (GameActive)
            Timer += Time.deltaTime;

        SliderTimer.value = TimeToLose - Timer;

        if (Timer >= TimeToLose)
        {
            if (RemainedMax - playerScore == 0)
                Lose();
            else
            {
                Win();
            }
        }
    }

    public void DecreaseScores()
    {
        if (GameActive)
        {
            playerScore--;
            RemainingTXT.text = playerScore.ToString() + " / " + RemainedMax.ToString();

            if (playerScore <= 0)
            {
                Win();
            }
        }
    }

    public void Win()
    {
        if (GameActive)
        {
            GameActive = false;
            WinPanel.Init(playerScore, RemainedMax);
            WinPanel.gameObject.SetActive(true);
        }
    }

    public void Lose()
    {
        if (GameActive)
        {
            GameActive = false;
            losePanel.SetActive(true);
        }
    }

    public void Reset(float x)
    {
        ball.GetComponent<Rigidbody2D>().Sleep();
        player.position = new Vector2(player.position.x, 0);
        //ball.position = new Vector2(0, 0);

        ball.GetComponent<Rigidbody2D>().WakeUp();
        Vector2 direction = new Vector2(1, Random.Range(1.5f, -1.5f));
        if (Random.Range(0, 2) == 1) direction.x *= -1;
        ball.GetComponent<Rigidbody2D>().AddForce(direction * startBallSpeed);
        GameActive = true;
    }
}