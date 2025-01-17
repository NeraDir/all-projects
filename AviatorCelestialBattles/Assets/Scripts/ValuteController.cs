using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ValuteController : MonoBehaviour
{
    public static ValuteController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public TMP_Text MoneyTXT;
    public TMP_Text ScoreTXT;

    public TMP_Text BestScoreTXT;

    public Transform WhereCreateMoneyAddTXT;
    public Transform WhereCreateScoreAddTXT;

    public TMP_Text AddMoneyTXTPrefab;
    public TMP_Text AddScoreTXTPrefab;

    public int MoneySave
    {
        get
        {
            if (!PlayerPrefs.HasKey("MoneySave"))
                return 0;

            return PlayerPrefs.GetInt("MoneySave");
        }
        set
        {
            PlayerPrefs.SetInt("MoneySave", value);
        }
    }

    public int BestScore
    {
        get
        {
            if (!PlayerPrefs.HasKey("ScoreSave"))
                return 0;

            return PlayerPrefs.GetInt("ScoreSave");
        }
        set
        {
            PlayerPrefs.SetInt("ScoreSave", value);
        }
    }

    public int ScoreCurrentSession = 0;

    private void Start()
    {
        if (ScoreTXT != null)
            ScoreTXT.text = ScoreCurrentSession.ToString();

        if (BestScoreTXT != null)
            BestScoreTXT.text = BestScore.ToString();

        MoneyTXT.text = MoneySave.ToString();
    }

    public void AddMoney(int num)
    {
        MoneySave += num;
        MoneyTXT.text = MoneySave.ToString();

        if (AddMoneyTXTPrefab != null)
        {
            var money = Instantiate(AddMoneyTXTPrefab, WhereCreateMoneyAddTXT.position, Quaternion.identity, WhereCreateMoneyAddTXT);

            if (num > 0)
            {
                money.text = "+ " + num;
            }
            else
            {
                money.text = "- " + num;
            }

            money.transform.DOMoveY(money.transform.position.y + 100f, 1f).OnComplete(() => Destroy(money.gameObject));
        }
    }

    public void AddScore(int num)
    {
        ScoreCurrentSession += num;
        ScoreTXT.text = ScoreCurrentSession.ToString();

        if (AddScoreTXTPrefab != null)
        {
            var score = Instantiate(AddScoreTXTPrefab, WhereCreateScoreAddTXT.position, Quaternion.identity, WhereCreateScoreAddTXT);

            if (num > 0)
            {
                score.text = "+ " + num;
            }
            else
            {
                score.text = "- " + num;
            }

            score.transform.DOMoveY(score.transform.position.y + 100f, 1f).OnComplete(() => Destroy(score.gameObject));
        }
    }
}
