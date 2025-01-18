using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class jackGameManager : MonoBehaviour
{
    public static int BetValue;

    public static int RerolCount;

    public static float score;

    public TMP_Text DisplayBalance;

    public TMP_Text DisplayRerolCount;

    public TMP_Text DisplayTotal;

    public TMP_Text DisplayScore;

    public TMP_Text DisplayEnemieScore;

    public TMP_Text DisplayPlayerLooseScore;

    public TMP_Text DispalyPlayerWinScore;

    public jackGameDiceComponent[] dices;

    public jackGameDiceComponent[] enemieDices;

    public GameObject loose;

    public GameObject win;

    public static bool canChangeBet;

    public static float temper = 0;
    public static float Enemietemper = 0;
    public static float EnemieScore = 0;

    public static bool canRerol;

    private bool isWhoWin;
    private float timer = 0;
    private void Start()
    {
        canRerol = false;
        isWhoWin = false;
        RerolCount = 3;
        score = 0;
        BetValue = 0;
        temper = 0;
        Enemietemper = 0;
        canChangeBet = false;
        EnemieScore = 0;
        jackGameDiceComponent.isLastPlaced.AddListener(IsEnd);
    }

    private void LateUpdate()
    {
        DisplayBalance.text = jackLoaderDiceComponent.BestScore.ToString();
        DisplayRerolCount.text = "X" + RerolCount.ToString();
        DisplayTotal.text = BetValue.ToString();
        score = Mathf.MoveTowards(score, temper, 10 * Time.deltaTime);
        EnemieScore = Mathf.MoveTowards(EnemieScore, Enemietemper, 10 * Time.deltaTime);
        DisplayEnemieScore.text = EnemieScore.ToString("0");
        DisplayScore.text = score.ToString("0");
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            jackLoaderDiceComponent.BestScore += 1;
            timer = 0;
        }
    }

    private void IsEnd() 
    {
        StartCoroutine(Waiting());
    }

    private IEnumerator Waiting() 
    {
        yield return new WaitForSeconds(0.5f);
        isWhoWin = EnemieScore > score ? true : false;
        foreach (var item in dices)
        {
            item.ReLaunch(false);
            yield return new WaitForSeconds(0.5f);
        }
        foreach (var item in enemieDices)
        {
            item.ReLaunch(false);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        if (isWhoWin)
        {
            DisplayPlayerLooseScore.text = BetValue.ToString();
            loose.SetActive(true);
        }
        else
        {
            DispalyPlayerWinScore.text = (BetValue * 2).ToString("0");
            win.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        jackGameDiceComponent.isLastPlaced.RemoveAllListeners();
    }

    public void OnClickRestart() 
    {
        if (isWhoWin)
        {
            jackLoaderDiceComponent.BestScore -= BetValue;
        }
        else
        {
            jackLoaderDiceComponent.BestScore += BetValue * 2;
        }
        SceneManager.LoadScene("Game");
    }

    public void OnClickMenu() 
    {
        if (isWhoWin)
        {
            jackLoaderDiceComponent.BestScore -= BetValue;
        }
        else
        {
            jackLoaderDiceComponent.BestScore += BetValue * 2;
        }
        SceneManager.LoadScene("Menu");
    }
}
