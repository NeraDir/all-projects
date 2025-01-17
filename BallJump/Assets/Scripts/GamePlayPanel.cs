using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text mettersText;

    [SerializeField]
    private TMP_Text coinsText;
    private float coinsCountLerp;

    [SerializeField]
    private List<HeartIcon> heartIcons;


    [SerializeField]
    private Transform ballTransform;

    private float maxBallMetters;
    private float maxBallMettersLerp;

    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private GameObject pausePanel;


    private bool ballInScene;

    private void Start()
    {
        maxBallMetters = 0;
        maxBallMettersLerp = 0;
        coinsCountLerp = 0;

        ballInScene = true;
    }


    private void OnEnable()
    {
        Ball.ObstacleDetected += DetetcDamageBall;
    }
    private void OnDisable()
    {
        Ball.ObstacleDetected -= DetetcDamageBall;
    }


    private void FixedUpdate()
    {
        if (ballInScene)
        {
            try
            {
                if (ballTransform.position.y > maxBallMetters)
                {
                    maxBallMetters = ballTransform.position.y;
                    BallConfigsController.currentMaxMetters = maxBallMetters;
                }
            }
            catch
            {

            }
        }


        maxBallMettersLerp = Mathf.Lerp(maxBallMettersLerp, maxBallMetters, 0.3f);
        coinsCountLerp = Mathf.Lerp(coinsCountLerp, BallConfigsController.coinCount, 0.3f);


        if (maxBallMettersLerp < 1)
        {
            mettersText.text = "0m";
        }
        else
        {
            mettersText.text = maxBallMettersLerp.ToString("#m");
        }

        if (coinsCountLerp < 1)
        {
            coinsText.text = "0";
        }
        else
        {
            coinsText.text = coinsCountLerp.ToString("#");
        }

       
    }


    public void DetetcDamageBall()
    {
        Debug.Log("BallConfigsController.ballHealth: " + BallConfigsController.ballHealth);
        if (BallConfigsController.ballHealth > 1)
        {
            BallConfigsController.ballHealth--;

            heartIcons[BallConfigsController.ballHealth].ChangeState(HeartIconState.Inactive);
            if (BallConfigsController.ballHealth == 1)
            {
                heartIcons[BallConfigsController.ballHealth + 1].ChangeState(HeartIconState.Block);
            }

        }
        else
        {
            Destroy(ballTransform.gameObject);
            ballInScene = false;
            ShowResultPanel();
        } 
    }

    public void ShowResultPanel()
    {
        gameObject.SetActive(false);
        resultPanel.SetActive(true);
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }


}
