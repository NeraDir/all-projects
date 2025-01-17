using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CleoCardLine 
{
    public List<Image> cleoCards;
}

public class CleoGameController : MonoBehaviour
{
    public GameObject cleoEndPanel;

    public static bool isLoose;

    private bool isDraw;

    public static int cleoScore;

    public Text cleoShowScore;

    public Text cleoShowRestartCount;

    public Text cleoShowRound;

    public Text cleoShowResultScore;

    public Text cleoShowResult;

    public Image cleoEnemieHealthBar;

    public Image cleoPlayerHealthBar;

    public Image cleoEnemieFace;

    public Sprite[] cleoCellsSprites;

    public Sprite[] cleoEnemiesCellsSprites;

    public Sprite[] cleoEnemiesSprites;

    public Image[] cleoEnemiesCells;

    private int cleoRestartCount;

    private int cleoRoundCount;

    public static float cleoPlayerHealthValue;

    public static float cleoEnemieHealthValue;

    public CleoCardLine[] cleoLines;

    private bool cleoRoundStarted = false;

    private void Start()
    {
        cleoScore = 0;
        cleoRoundCount = 1;
        isDraw = false;
        isLoose = false;
        cleoRoundStarted = false;
        cleoEnemieHealthValue = 5;
        cleoPlayerHealthValue = 5;
        cleoRestartCount = 3;
        cleoEnemieFace.sprite = cleoEnemiesSprites[Random.Range(0, cleoEnemiesSprites.Length)];
        OnClickCleoRestartCards(false);
        OnSetEnemiesCells();
    }

    private void LateUpdate()
    {
        cleoShowScore.text = cleoScore.ToString();
        cleoShowRestartCount.text = "X" + cleoRestartCount.ToString();
        cleoPlayerHealthBar.fillAmount = Mathf.Lerp(cleoPlayerHealthBar.fillAmount, (cleoPlayerHealthValue / 5), 8 * Time.deltaTime);
        cleoEnemieHealthBar.fillAmount = Mathf.Lerp(cleoEnemieHealthBar.fillAmount, (cleoEnemieHealthValue / 5), 8 * Time.deltaTime);
        cleoShowRound.text = "ROUND " + cleoRoundCount.ToString();
        cleoShowResultScore.text = cleoScore.ToString();
        if (cleoScore > CleoMenuManager.CleoBestScoreValue)
        {
            CleoMenuManager.CleoBestScoreValue = cleoScore;
        }
    }

    private void CheckGameState()
    {
        if (cleoEnemieHealthValue <= 0 && cleoPlayerHealthValue > 0)
        {
            cleoEndPanel.SetActive(true);
            isLoose = false;
            cleoShowResult.text = "WIN";
        }
        else if (cleoEnemieHealthValue > 0 && cleoPlayerHealthValue <= 0)
        {
            cleoEndPanel.SetActive(true);
            isLoose = true;
            cleoShowResult.text = "LOOSE";
        }
        else if (cleoEnemieHealthValue > 0  && cleoPlayerHealthValue > 0 && cleoLines[0].cleoCards[0] == null)
        {
            cleoEndPanel.SetActive(true);
            isLoose = false;
            isDraw = true;
            cleoShowResult.text = "DRAW";
        }
        else if (cleoEnemieHealthValue <= 0 && cleoPlayerHealthValue <= 0 && cleoLines[0].cleoCards[0] == null)
        {
            cleoEndPanel.SetActive(true);
            isLoose = false;
            isDraw = true;
            cleoShowResult.text = "DRAW";
        }
        else if (cleoEnemieHealthValue <= 0 && cleoPlayerHealthValue <= 0)
        {
            cleoEndPanel.SetActive(true);
            isLoose = false;
            isDraw = true;
            cleoShowResult.text = "DRAW";
        }
    }

    private void OnSetEnemiesCells() 
    {
        foreach (var item in cleoEnemiesCells)
        {
            item.sprite = cleoEnemiesCellsSprites[Random.Range(0, cleoEnemiesCellsSprites.Length)];
        }
    }

    public void OnClickCleoStartRound() 
    {
        if (cleoRoundStarted)
            return;
        cleoRoundStarted = true;
        StartCoroutine(RoundMove());
    }

    private IEnumerator RoundMove() 
    {
        for (int i = 0; i < cleoLines.Length; i++)
        {
            cleoLines[i].cleoCards[cleoLines[i].cleoCards.Count - 1].transform.DOMove(cleoEnemiesCells[i].transform.position, 1.5f).OnComplete(() => OnCompleteee(cleoLines[i].cleoCards[cleoLines[i].cleoCards.Count - 1], cleoEnemiesCells[i],i));
            yield return new WaitForSeconds(1.6f);
        }
    }

    private void OnCompleteee(Image playerSprite,Image enemieSprite,int index) 
    {
        if (playerSprite.sprite.name.Contains("ничего") && enemieSprite.sprite.name.Contains("меч"))
        {
            cleoPlayerHealthValue--;
            cleoScore -= Random.Range(5, 10);
        }
        else if (playerSprite.sprite.name.Contains("меч") && enemieSprite.sprite.name.Contains("ничего"))
        {
            cleoEnemieHealthValue--;
            cleoScore += Random.Range(5, 10);
        }
        else if (playerSprite.sprite.name.Contains("меч") && enemieSprite.sprite.name.Contains("меч"))
        {
            cleoEnemieHealthValue--;
            cleoPlayerHealthValue--;
            cleoScore += Random.Range(5, 10);
        }
        else if (playerSprite.sprite.name.Contains("сердце") && enemieSprite.sprite.name.Contains(" "))
        {
            cleoPlayerHealthValue++;
            if (cleoPlayerHealthValue >= 5)
            {
                cleoPlayerHealthValue = 5;
            }
        }
        playerSprite.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(playerSprite.gameObject); cleoLines[index].cleoCards.Remove(playerSprite); });
        enemieSprite.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => enemieSprite.transform.DOScale(Vector3.one, 0.25f).OnComplete(() => 
        {
            if (index == 4)
            {
                cleoRoundStarted = false;
                cleoRoundCount++;
                CheckGameState();
                OnSetEnemiesCells();
            }
        }));
        
    }

    public void OnClickCleoRestartCards(bool iser)
    {
        if (cleoRoundStarted)
            return;
        if (cleoRestartCount <= 0)
            return;
        if (iser)
            cleoRestartCount--;
        foreach (var item in cleoLines)
        {
            int indexOfSprite = Random.Range(0, cleoCellsSprites.Length);
            foreach (var item2 in item.cleoCards)
            {
                item2.sprite = cleoCellsSprites[indexOfSprite];
            }
        }
    }

    public void OnClickCleoRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickCleoMenu() 
    {
        SceneManager.LoadScene("CleoLoading");
    }


}
