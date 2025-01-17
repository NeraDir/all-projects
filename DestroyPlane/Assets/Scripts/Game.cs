using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    public static int health;
    public static int maxHealth;

    public static PlaneColor targetPlaneColor;

    public static int lastLevelColorIndex;
    public static int currentLevelColorIndex;

    public static int lastLevelGameOverStateIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("lastLevelGameOverStateIndex"))
            {
                return PlayerPrefs.GetInt("lastLevelGameOverStateIndex");
            }

            PlayerPrefs.SetInt("lastLevelGameOverStateIndex", 0);
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("lastLevelGameOverStateIndex", value);
        }
    }


    [SerializeField]
    private TMP_Text targetDisplay;
    [SerializeField]
    private TMP_Text healthDisplay;
    [SerializeField]
    private TMP_Text timerDisplay;

    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private float levelTime;

    private void OnEnable()
    {
        TargetPlaneDetecter.TargetPlaneDetectedEvent += DicrementHealth;
        Init();
    }
    private void OnDisable()
    {
        TargetPlaneDetecter.TargetPlaneDetectedEvent -= DicrementHealth;
    }


    public void Init()
    {
        maxHealth = 3;
        health = maxHealth;

        targetPlaneColor = GetTargetPlaneColor();
        Debug.Log("TargetColor: " + targetPlaneColor.ToString());
        StartCoroutine(timer());
    }



    private void Update()
    {
        healthDisplay.text = health + "/" + maxHealth;
        timerDisplay.text = levelTime.ToString("#.#s");
    }

    public PlaneColor GetTargetPlaneColor()
    {

        if (!PlayerPrefs.HasKey("LasrTargetPlaneColorSave"))
            PlayerPrefs.SetInt("LasrTargetPlaneColorSave", 0);
        
        int lastColorIndex = PlayerPrefs.GetInt("LasrTargetPlaneColorSave");


        int newColorIndex = 0;

        if (lastLevelGameOverStateIndex == 0)
        {
            newColorIndex = Random.Range(0, 3);

            while (lastColorIndex == newColorIndex)
                newColorIndex = Random.Range(0, 3);
        }
        else
        {
            lastLevelGameOverStateIndex = 0;
            newColorIndex = lastColorIndex;
        }




        currentLevelColorIndex = newColorIndex;

        if (newColorIndex == 0)
        {
            //78CDFF
            targetDisplay.text = targetDisplay.text.Replace("FFFFFF", "78CDFF");
            targetDisplay.text = targetDisplay.text.Replace("blue", "blue");
            return PlaneColor.Blue;
        }

        else if (newColorIndex == 1)
        {
            //FF7883
            targetDisplay.text = targetDisplay.text.Replace("FFFFFF", "FF7883");
            targetDisplay.text = targetDisplay.text.Replace("blue", "red");
            return PlaneColor.Red;
        }

        else if (newColorIndex == 2)
        {
            //FFB378
            targetDisplay.text = targetDisplay.text.Replace("FFFFFF", "FFB378");
            targetDisplay.text = targetDisplay.text.Replace("blue", "orange");
            return PlaneColor.Orange;
        }

        else
        {
            //80FF78
            targetDisplay.text = targetDisplay.text.Replace("FFFFFF", "80FF78");
            targetDisplay.text = targetDisplay.text.Replace("blue", "green");
            return PlaneColor.Green;
        }
        
    }

    public void DicrementHealth()
    {
        if(health - 1 > 0)
        {
            health--;
        }
        else
        {
            ShowLosePanel();
        }
    }


    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }
    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }


    private IEnumerator timer()
    {
        

        while (levelTime > 0f)
        {
            levelTime -= Time.deltaTime;
            yield return null;
        }

        ShowWinPanel();
    }
}
