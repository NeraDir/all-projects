using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    public List<CandyItem> candyItems;
    public List<PiramydCell> piramydCells;

    public TMP_Text scoreTMP;

    public TapItem tapItem;
    private int targetPiramydCellIndex = 0;

    public static int scorecount;
    public static int recordcountscore
    {
        get
        {
            string key = "keyrecordcountscore";

            if (!PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.SetInt(key, 0);
            }

            return PlayerPrefs.GetInt(key);
        }
        set
        {
            string key = "keyrecordcountscore";
            PlayerPrefs.GetInt(key);
        }
    }

    public GameObject gameOverWindow;
    public GameObject gameWinWindow;


    public static int candyIndex
    {
        get
        { 

            if (!PlayerPrefs.HasKey("keycandyIndex"))
            {
                PlayerPrefs.SetInt("keycandyIndex", 0);
            }

            return PlayerPrefs.GetInt("keycandyIndex");
        }
        set
        {
            PlayerPrefs.GetInt("keycandyIndex");
        }
    }
    public static int candyRewardValue
    {
        get
        {

            if (!PlayerPrefs.HasKey("keycandyRewardValue"))
            {
                PlayerPrefs.SetInt("keycandyRewardValue", 70);
            }

            return PlayerPrefs.GetInt("keycandyRewardValue");
        }
        set
        {
            PlayerPrefs.GetInt("keycandyRewardValue");
        }
    }
    public static string playerRang;


    private void OnEnable()
    {
        tapItem.gameObject.SetActive(false);
        TapItem.TapToCellCompleteEvent += UpdateTargetCell;
        TapItem.TapIsZeroScaleEvent += GameOver;


    }
    private void OnDisable()
    {
        TapItem.TapToCellCompleteEvent -= UpdateTargetCell;
        TapItem.TapIsZeroScaleEvent -= GameOver;
    }

    public IEnumerator startGame()
    {
        yield return new WaitForSeconds(3.5f);

        tapItem.gameObject.SetActive(true);
        SetNextItemToTapCell();
        tapItem.StartDicrementScale();
    }

    private void Start()
    {
        scorecount = 0;
        FillPiramyd();
        StartCoroutine(startGame());

    }

    private void Update()
    {
        scoreTMP.text = "SCORE\n" + (scorecount == 0? "0": scorecount.ToString());
    }


    private void SetNextItemToTapCell()
    {
        tapItem.SetItem(piramydCells[targetPiramydCellIndex].GetCandyItem());
        
    }

    private void FillPiramyd()
    {

        int itemCountInLine = 5;
        int itemIndex = 0;
        
        for (int i = 0; i < piramydCells.Count; )
        {
            Debug.Log(itemIndex);
            CandyItem candyItem = candyItems[itemIndex];

            for (int j = i; j < i + itemCountInLine; j++)
            {
                piramydCells[j].Init(candyItem);
            }

            i += itemCountInLine;

            itemCountInLine--;
            itemIndex++;

        }
        
    }

    public void UpdateTargetCell()
    {


        Debug.Log("targetPiramydCellIndex: " + targetPiramydCellIndex);
        if (targetPiramydCellIndex == 14)
        {
            Debug.Log("AAAAAA");
            GameWin();
            return;
        }
        else
        {
            StartCoroutine(updateTargetCell());
        }

     
    }


    private IEnumerator updateTargetCell()
    {
        scorecount += 100;
        tapItem.HideMe();
        piramydCells[targetPiramydCellIndex].ActivateCell();
        targetPiramydCellIndex++;
        yield return new WaitForSeconds(3f);


        SetNextItemToTapCell();
        tapItem.ShowMe();
        tapItem.StartDicrementScale();
    }


    public void GameWin()
    {
        SaveRecord();
        gameWinWindow.SetActive(true);
    }
    public void GameOver()
    {
        SaveRecord();
        gameOverWindow.SetActive(true);
    }

    public void ReloadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadGameMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }


    private void SaveRecord()
    {
        if (scorecount > recordcountscore)
            recordcountscore = scorecount;
    }
}
