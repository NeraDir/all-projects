using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mathManager : MonoBehaviour
{
    public TMP_Text firstNumberS;
    public TMP_Text secondNumberS;
    public TMP_Text thirdNumberS;
    public TMP_Text totalNumberS;

    public TMP_Text firstSymbolS;
    public TMP_Text secondSymbolS;

    public TMP_Text[] scoreShow;

    public Image clockImage;
    public Transform arrowofTime;

    private int firstNum;
    private int secondNum;
    private int thirdNum;
    private int totalNum;

    private int needthirdNum;

    public static int bestScore 
    {
        get 
        {
            if (PlayerPrefs.HasKey("MathPantherSave"))
            {
                return PlayerPrefs.GetInt("MathPantherSave");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("MathPantherSave", value);
        }
    }

    public static int pantherTryCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("pantherTryCountssaves"))
            {
                return PlayerPrefs.GetInt("pantherTryCountssaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("pantherTryCountssaves", value);
        }
    }

    public static string panthermathName;

    public static int pantherMathWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("pantherMathWinsCountSave"))
            {
                return PlayerPrefs.GetInt("pantherMathWinsCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("pantherMathWinsCountSave", value);
        }
    }

    public static int currentScore;

    public char[] symbols;

    private char firtstSymbol;
    private char secondSymbol;

    public Sprite beginColor;
    public Sprite endColor;

    private float timeToGetAnswer;

    public AudioClip clickSound;

    public AudioSource clickSource;

    private float beginTime = 7.5f;

    public GameObject great;
    public GameObject bad;

    public Transform spawnPos;

    public GameObject totalScreen;

    private void Start()
    {
        currentScore = 0;
        timeToGetAnswer = beginTime;
        SetNewValues();
        StartCoroutine(TimeController());
    }

    private IEnumerator TimeController() 
    {
        while (timeToGetAnswer > 0)
        {
            yield return new WaitForSeconds(1);
            arrowofTime.Rotate(new Vector3(0, 0, -1), 360 / beginTime);
            timeToGetAnswer -= 1;
            clickSource.PlayOneShot(clickSound);
            if (timeToGetAnswer <= beginTime / 2)
            {
                clockImage.sprite = endColor;
            }
            else
            {
                clockImage.sprite = beginColor;
            }
        }
        totalScreen.SetActive(true);
        StopAllCoroutines();
    }

    private void SetNewValues() 
    {
        firstNum = Random.Range(0, 10);
        secondNum = Random.Range(0, 10);
        needthirdNum = Random.Range(0, 10);
        firtstSymbol = symbols[Random.Range(0, symbols.Length)];
        secondSymbol = symbols[Random.Range(0, symbols.Length)];
        if (firtstSymbol == '+' && secondSymbol == '+')
        {
            totalNum = firstNum + secondNum + needthirdNum;
        }
        else if (firtstSymbol == '-' && secondSymbol == '+')
        {
            totalNum = firstNum - secondNum + needthirdNum;
        }
        else if (firtstSymbol == '-' && secondSymbol == '-')
        {
            totalNum = firstNum - secondNum - needthirdNum;
        }
        else if (firtstSymbol == '+' && secondSymbol == '-')
        {
            totalNum = firstNum + secondNum - needthirdNum;
        }
        thirdNum = 0;
    }

    public void ChangeThirdNumber() 
    {
        thirdNum++;
        if (thirdNum > 10)
        {
            thirdNum = 0;
        }
    }

    public void CheckSumm() 
    {
        if (thirdNum == needthirdNum)
        {
            Debug.Log("true");
            arrowofTime.rotation = Quaternion.Euler(0, 0, 0);
            beginTime += 4;
            if (beginTime >= 15)
            {
                beginTime = 15;
            }
            StopAllCoroutines();
            timeToGetAnswer = beginTime;
            currentScore += Random.Range(100, 500);
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
            }
            SetNewValues();
            Instantiate(great, spawnPos);
            StartCoroutine(TimeController());
        }
        else
        {
            Debug.Log("false");
            Instantiate(bad, spawnPos);
            SetNewValues();
        }
    }

    private void LateUpdate()
    {
        firstNumberS.text = firstNum.ToString("0");
        secondNumberS.text = secondNum.ToString("0");
        thirdNumberS.text = thirdNum.ToString("0");
        totalNumberS.text = totalNum.ToString("0");
        firstSymbolS.text = firtstSymbol.ToString();
        secondSymbolS.text = secondSymbol.ToString();
        foreach (var item in scoreShow)
        {
            item.text = currentScore.ToString("0");
        }
    }


    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu() 
    {
        SceneManager.LoadScene("Menu");
    }

}
