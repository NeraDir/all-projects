using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int avikDataOfUserCanvasScale
    {
        get
        {
            if (PlayerPrefs.HasKey("avikDataOfUserCanvasScaleSave"))
            {
                return PlayerPrefs.GetInt("avikDataOfUserCanvasScaleSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("avikDataOfUserCanvasScaleSave", value);
        }
    }

    public static int avikBestScoreValue 
    {
        get
        {
            if (PlayerPrefs.HasKey("avikBestScoreValueSave"))
            {
                return PlayerPrefs.GetInt("avikBestScoreValueSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("avikBestScoreValueSave", value);
        }
    }

    public static string developmingstringKey;

    public static int avikDataOfEnetersCount
    {
        get
        {
            if (PlayerPrefs.HasKey("avikDataOfEnetersCountSave"))
            {
                return PlayerPrefs.GetInt("avikDataOfEnetersCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("avikDataOfEnetersCountSave", value);
        }
    }

    public TMP_Text[] ScoreTXT;

    public PostProcessVolume PostVolume;

    private Vignette vignette;

    private LensDistortion lensDistortion;

    public TMP_Text[] CurrentDistanceTXT;

    public TMP_Text NeedDistanceTXT;

    public TMP_Text SpeedTXT;

    public TMP_Text NeedTimeTXT;

    public TMP_Text[] CurrentTimeTXT;

    public GameObject attentionTxt;

    public Slider DistanceFillBar;

    private OrderData currentOrder;

    public OrderController OrderController;

    public GameObject WiningPanel;

    public GameObject LoosingPanel;

    public Color defaultVigneeteColor;

    public Color criticalVigneteColor;

    public AudioSource AttentionPlayer;

    private int score;

    private float currentDistance;

    private float currentTime;

    private float currentSpeed;

    private bool gameInited;

    public static bool isCriticalStatus;

    private bool isDownGo = false;

    private float defaulSpeed;

    private void Awake()
    {
        currentOrder = new OrderData(Random.Range(15f,400f),Random.Range(60,240));
        currentOrder.NeedTime = currentOrder.NeedDistance / 2.5f;
        currentSpeed = currentOrder.NeedDistance / 4;
        DistanceFillBar.maxValue = currentOrder.NeedDistance;
        DistanceFillBar.value = currentDistance;
        vignette = PostVolume.profile.GetSetting<Vignette>();
        lensDistortion = PostVolume.profile.GetSetting<LensDistortion>();
        vignette.intensity.value = 0.529f;
        lensDistortion.intensity.value = -42.2f;
        isDownGo = false;
        gameInited = true;
        isCriticalStatus = false;
        defaulSpeed = currentSpeed;
    }

    private void Update()
    {
        if (!gameInited)
            return;
        currentDistance +=( ((currentSpeed / 10)) * Time.deltaTime);

        if ((int)currentTime % 2 == 0 && (int)currentTime != 0)
        {
            score += Random.Range(2, 3);
            if (avikBestScoreValue < score)
            {
                avikBestScoreValue = score;
            }
        }

        currentTime += Time.deltaTime;
        if (currentOrder.NeedTime >= currentTime && currentOrder.NeedDistance <= currentDistance)
        {
            WiningPanel.SetActive(true);
            lensDistortion.intensity.value = 0;
            gameInited = false;
        }

        if (currentOrder.NeedDistance <= currentDistance && currentOrder.NeedTime < currentTime)
        {
            LoosingPanel.SetActive(true);
            lensDistortion.intensity.value = 0;
            gameInited = false;
        }

        foreach (var item in ScoreTXT)
        {
            item.text = score.ToString() + "B";
        }

        foreach (var item in CurrentDistanceTXT)
        {
            item.text = currentDistance.ToString("0.0") + "km";
        }

        foreach (var item in CurrentTimeTXT)
        {
            item.text = currentTime.ToString("0") + "s";
        }
        SpeedTXT.text = currentSpeed.ToString("0") + "km/h";
        NeedTimeTXT.text = currentOrder.NeedTime.ToString("0") + "s";
        NeedDistanceTXT.text = currentOrder.NeedDistance.ToString("0.0") + "km";
        DistanceFillBar.value = currentDistance;

        if (isCriticalStatus)
        {

            currentSpeed = defaulSpeed / 2;
            if (!isDownGo)
            {
                vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, 0.61f, 0.05f * Time.deltaTime);
                vignette.color.value = Color.Lerp(vignette.color.value, criticalVigneteColor, 2 * Time.deltaTime);
                Debug.Log(vignette.intensity.value);
                if (vignette.intensity.value >= 0.6f)
                {
                    Debug.Log("Down");
                    isDownGo = true;
                }
            }
            else
            {
                vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, 0.510f, 0.05f * Time.deltaTime);
                vignette.color.value = Color.Lerp(vignette.color.value, defaultVigneeteColor, 2 * Time.deltaTime);
                Debug.Log(vignette.intensity.value);
                if (vignette.intensity.value < 0.529f)
                {
                    Debug.Log("Up");
                    isDownGo = false;
                }
            }
            attentionTxt.SetActive(true);
        }
        else
        {
            vignette.intensity.value = 0.529f;
            vignette.color.value = defaultVigneeteColor;
            currentSpeed = defaulSpeed;
            attentionTxt.SetActive(false);
        }
    }

    public void OnReplayPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenuPressed()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnNextPressed() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
