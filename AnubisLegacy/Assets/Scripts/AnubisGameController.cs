using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CellLine
{
    public List<Image> cards;
}

public class AnubisGameController : MonoBehaviour
{
    public GameObject rulesPage;

    public Text prelevelTxt;

    public GameObject nextButton;

    public GameObject prelevelPage;

    public GameObject anubisEndPanel;

    public static bool isLoose;

    private bool isDraw;

    public static int Score;

    public Text scoreTxt;

    public Text restartCountTxt;

    public Text roundTxt;

    public Text resultScoreTxt;

    public Text resultTxt;

    public Image enemieHeathImage;

    public Image playerHealthImage;

    public Image enemieAvatar;

    public Sprite[] cellSprites;

    public Sprite[] enemieCellSprites;

    public Sprite[] enemieSprites;

    public Image[] enemieCells;

    private int _restartCount;

    private int _roundCount;

    public static float PlayerHealth;

    public static float EnemieHealth;

    public CellLine[] cellLines;

    private bool _roundStarted = false;

    private void Start()
    {
        prelevelTxt.text = "LEVEL - " + (AnubisUserData.CurrentLevel + 1).ToString();
        prelevelPage.SetActive(true);
        Invoke(nameof(DisplayRules), 2);
        Score = 0;
        _roundCount = 1;
        isDraw = false;
        isLoose = false;
        _roundStarted = false;
        EnemieHealth = 5;
        PlayerHealth = 5;
        _restartCount = 3;
        enemieAvatar.sprite = enemieSprites[Random.Range(0, enemieSprites.Length)];
        OnChangeCellsButtonPressed(false);
        SetEnemieCells();
    }

    private void DisplayRules()
    {
        if (!PlayerPrefs.HasKey("AnubisGameRulesDisplayed"))
        {
            rulesPage.SetActive(true);
            PlayerPrefs.SetInt("AnubisGameRulesDisplayed", 1);
        }
    }

    public void OnClickCloseRules()
    {
        rulesPage.SetActive(false);
    }

    private void LateUpdate()
    {
        scoreTxt.text = Score.ToString();
        restartCountTxt.text = "X" + _restartCount.ToString();
        playerHealthImage.fillAmount = Mathf.Lerp(playerHealthImage.fillAmount, (PlayerHealth / 5), 8 * Time.deltaTime);
        enemieHeathImage.fillAmount = Mathf.Lerp(enemieHeathImage.fillAmount, (EnemieHealth / 5), 8 * Time.deltaTime);
        roundTxt.text = "ROUND " + _roundCount.ToString();
        resultScoreTxt.text = Score.ToString();
        if (Score > AnubisUserData.BestScore)
        {
            AnubisUserData.BestScore = Score;
        }
    }

    private void CheckGameState()
    {
        if (EnemieHealth <= 0 && PlayerHealth > 0)
        {
            anubisEndPanel.SetActive(true);
            isLoose = false;
            resultTxt.text = "WIN";
            nextButton.SetActive(true);
            AnubisUserData.Coins += 200;
        }
        else if (EnemieHealth > 0 && PlayerHealth <= 0)
        {
            anubisEndPanel.SetActive(true);
            isLoose = true;
            resultTxt.text = "LOOSE";
            nextButton.SetActive(false);
            AnubisUserData.Coins -= 50;
        }
        else if (EnemieHealth > 0 && PlayerHealth > 0 && cellLines[0].cards[0] == null)
        {
            anubisEndPanel.SetActive(true);
            isLoose = false;
            isDraw = true;
            resultTxt.text = "DRAW";
            nextButton.SetActive(true);
            AnubisUserData.Coins += 100;
        }
        else if (EnemieHealth <= 0 && PlayerHealth <= 0 && cellLines[0].cards[0] == null)
        {
            anubisEndPanel.SetActive(true);
            isLoose = false;
            isDraw = true;
            resultTxt.text = "DRAW";
            nextButton.SetActive(true);
            AnubisUserData.Coins += 100;
        }
        else if (EnemieHealth <= 0 && PlayerHealth <= 0)
        {
            anubisEndPanel.SetActive(true);
            isLoose = false;
            isDraw = true;
            resultTxt.text = "DRAW";
            nextButton.SetActive(true);
            AnubisUserData.Coins += 100;
        }
    }

    private void SetEnemieCells()
    {
        foreach (var item in enemieCells)
        {
            item.sprite = enemieCellSprites[Random.Range(0, enemieCellSprites.Length)];
        }
    }

    public void OnLaunchAnubisButtonPressed()
    {
        if (_roundStarted)
            return;
        _roundStarted = true;
        StartCoroutine(LaunchRound());
    }

    private IEnumerator LaunchRound()
    {
        for (int i = 0; i < cellLines.Length; i++)
        {
            cellLines[i].cards[cellLines[i].cards.Count - 1].transform.DOMove(enemieCells[i].transform.position, 1.5f).OnComplete(() => OnComplete(cellLines[i].cards[cellLines[i].cards.Count - 1], enemieCells[i], i));
            yield return new WaitForSeconds(1.6f);
        }
    }

    private void OnComplete(Image playerSprite, Image enemieSprite, int index)
    {
        if (playerSprite.sprite.name.Contains("el6") && enemieSprite.sprite.name.Contains("el2"))
        {
            PlayerHealth--;
            Score -= Random.Range(5, 10);
        }
        else if (playerSprite.sprite.name.Contains("el7") && enemieSprite.sprite.name.Contains("el2"))
        {
            PlayerHealth--;
            Score -= Random.Range(5, 10);
        }
        else if (playerSprite.sprite.name.Contains("el2") && playerSprite.sprite.name.Contains("el6"))
        {
            EnemieHealth--;
            Score += Random.Range(5, 10);
        }
        else if (playerSprite.sprite.name.Contains("el2") && (playerSprite.sprite.name.Contains("el7")))
        {
            EnemieHealth--;
            Score += Random.Range(5, 10);
        }
        else if (playerSprite.sprite.name.Contains("el2") && enemieSprite.sprite.name.Contains("el2"))
        {
            EnemieHealth--;
            PlayerHealth--;
            Score += Random.Range(5, 10);
        }
        else if (playerSprite.sprite.name.Contains("el4") && (playerSprite.sprite.name.Contains("el7")))
        {
            PlayerHealth++;
            if (PlayerHealth >= 5)
            {
                PlayerHealth = 5;
            }
        }
        else if (playerSprite.sprite.name.Contains("el4") && playerSprite.sprite.name.Contains("el6"))
        {
            PlayerHealth++;
            if (PlayerHealth >= 5)
            {
                PlayerHealth = 5;
            }
        }
        playerSprite.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(playerSprite.gameObject); cellLines[index].cards.Remove(playerSprite); });
        enemieSprite.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => enemieSprite.transform.DOScale(Vector3.one, 0.25f).OnComplete(() =>
        {
            if (index == 4)
            {
                _roundStarted = false;
                _roundCount++;
                CheckGameState();
                SetEnemieCells();
            }
        }));

    }

    public void OnChangeCellsButtonPressed(bool iser)
    {
        if (_roundStarted)
            return;
        if (_restartCount <= 0)
            return;
        if (iser)
            _restartCount--;
        foreach (var item in cellLines)
        {
            int indexOfSprite = Random.Range(0, cellSprites.Length);
            foreach (var item2 in item.cards)
            {
                item2.sprite = cellSprites[indexOfSprite];
            }
        }
    }

    public void OnAnubisNextButtonPressed()
    {
        AnubisUserData.CurrentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnAnubisGameRestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnAnubisLoadMenuButtonPressed()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Scene nextScene = SceneManager.CreateScene("AnubisLegacyMenuScene");
        SceneManager.SetActiveScene(nextScene);
        GameObject menuObject = Resources.Load("Prefabs/AnubisMenu") as GameObject;
        Instantiate(menuObject);
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
