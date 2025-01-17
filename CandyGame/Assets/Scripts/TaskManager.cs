using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TaskManager : MonoBehaviour
{
    public Action Win;
    public static Action Lost;


    public int candyAmount;
    [SerializeField] private TMP_Text candyAmountText;
    [SerializeField] private TMP_Text candyAmountCompletedText;
    [SerializeField] private Image typeOfCandy;
    public int candyTypeId;
    [SerializeField] private List<Sprite> sprites;
    public int candyAmountComplete = 0;
    public float timer;
    [SerializeField] private TMP_Text timerText;
    public int TaskMoney;


    private void Awake()
    {
        Time.timeScale = 1;
        CandyGenerator.TaskComplete += LevelComplete;
        candyAmount = (PlayerPrefs.GetInt("level") / 5) + 1;
        candyAmountText.text = candyAmount.ToString();
        typeOfCandy.sprite = sprites[PlayerPrefs.GetInt("level") % 5];
        Debug.Log(sprites[PlayerPrefs.GetInt("level") % 5]);
        //typeOfCandy.transform.localScale = Vector3.one * 0.02f;
        candyTypeId = (PlayerPrefs.GetInt("level") % 5 + 3);
    }

    private void Update()
    {
        candyAmountCompletedText.text = candyAmountComplete.ToString();
        timerText.text = Watch((int)timer);
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            Lost.Invoke();
        }
    }

    public void LevelComplete()
    {
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        Win.Invoke();
        MoneyManager.SetMoney(TaskMoney);
    }

    public static string Watch(in int time)
    {

        return $"{time / 60}:{time % 60:D2}";
    }

    private void OnDestroy()
    {
        CandyGenerator.TaskComplete -= LevelComplete;
    }
}
