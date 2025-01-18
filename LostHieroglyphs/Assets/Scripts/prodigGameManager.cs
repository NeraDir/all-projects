using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class prodigGameManager : MonoBehaviour
{
    [SerializeField]
    private Image timerFillingBar;

    private float timer = 15;

    [SerializeField]
    private UILineConnector lineRenderer;

    [SerializeField]
    private UILineRenderer lineRend;

    public static UILineRenderer liner;

    public static List<Sprite> needSpritesCombination = new List<Sprite>();

    public static List<prodigCellComponent> currentSpritesCombination = new List<prodigCellComponent>();

    public static List<RectTransform> currentSpritesTransforms = new List<RectTransform>();

    [SerializeField]
    private GameObject winPanel;

    [SerializeField]
    private GameObject loosePanel;

    public static bool loose;

    public static bool win;

    public static int maxscore
    {
        get
        {
            if (PlayerPrefs.HasKey("prodigMaxScoreSave"))
            {
                return PlayerPrefs.GetInt("prodigMaxScoreSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("prodigMaxScoreSave", value);
        }
    }

    public static int score 
    {
        get 
        {
            if (PlayerPrefs.HasKey("prodigScoreSave"))
            {
                return PlayerPrefs.GetInt("prodigScoreSave");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("prodigScoreSave", value);
        }
    }

    [SerializeField]
    private TMP_Text[] scoreTxt;

    private void Start()
    {
        currentSpritesCombination = new List<prodigCellComponent>();
        currentSpritesTransforms = new List<RectTransform>();
        needSpritesCombination = new List<Sprite>();
        win = false;
        loose = false;
        liner = lineRend;
        timer = 15;
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        /*lineRenderer.transforms = currentSpritesTransforms.ToArray();*/
        if (timer <= 0 && !win)
        {
            loose = true;
        }
        UpdateTimerFillingBar();
        foreach (var item in scoreTxt)
        {
            item.text = "x" + score.ToString();
        }
        if (maxscore < score)
        {
            maxscore = score;
        }

        if (loose)
        {
            loosePanel.SetActive(true);
        }

        if (win)
        {
            winPanel.SetActive(true);
        }
    }

    public void Restart() 
    {
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu() 
    {
        score = 0;
        SceneManager.LoadScene("Mneu");
    }

    public void Next() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateTimerFillingBar() 
    {
        timerFillingBar.fillAmount = Mathf.Lerp(timerFillingBar.fillAmount, timer / 15, 10 * Time.deltaTime);
    }
}
