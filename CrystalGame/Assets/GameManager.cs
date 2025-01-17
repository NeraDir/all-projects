using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeToPlay;

    [SerializeField]
    private TMP_Text[] scoreText;
    [SerializeField]
    private TMP_Text timeLeftText;

    [SerializeField]
    private TMP_Text[] maxlvl;


    [SerializeField]
    private Image bgMan;

    [SerializeField]
    private Sprite[] manSprites;

    public static CrystalColor targetColor;

    public static int scoreCount;
    private float scoreCountLerp;

    private float leftTime;

    [SerializeField]
    private GameObject resultPanel;

    private int currentLvl;


    private void Update()
    {
        if (scoreCount <= 0)
        {
            scoreCount = 0;
        }
        scoreCountLerp = Mathf.Lerp(scoreCountLerp, scoreCount, 0.3f);

        foreach (var item in scoreText)
        {
            item.text = "SCORE " + "<color=#ffbd00>" + scoreCountLerp.ToString("0") + "</color>";
        }
        foreach (var item in maxlvl)
        {
            item.text = $"LVL <color=#aa00ff>{PlayerDatasSaver.countOfPressedNext}</color>";
        }

        if (PlayerDatasSaver.countOfPressedNext > PlayerDatasSaver.maxresearchedLVl)
        {
            PlayerDatasSaver.maxresearchedLVl = PlayerDatasSaver.countOfPressedNext;
        }
    }

    private void OnEnable()
    {
        scoreCount = 0;
        int indexofCrystall = Random.Range(0, 4);
        targetColor = (CrystalColor)indexofCrystall;
        bgMan.sprite = manSprites[indexofCrystall];
        StartCoroutine(timer());
    }

    private IEnumerator timer()
    {
        leftTime = timeToPlay;

        while (leftTime > 0.0f)
        {
            leftTime -= Time.deltaTime;
            timeLeftText.text = "TIME " + "<color=#ffbd00>" + leftTime.ToString("0") + "s" + "</color>";
            yield return null;
        }
        resultPanel.SetActive(true);
    }

    public void OnClickRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu() 
    {
        PlayerDatasSaver.countOfPressedNext = 0;
        SceneManager.LoadScene("SceneMenu");
    }

    public void OnClickNext() 
    {
        PlayerDatasSaver.countOfPressedNext++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnApplicationQuit()
    {
        PlayerDatasSaver.countOfPressedNext = 0;
    }
}
