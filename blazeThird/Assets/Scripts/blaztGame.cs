using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class blaztGame : MonoBehaviour
{
    public static int blaztfusionBestRecordCount
    {
        get
        {
            if (PlayerPrefs.HasKey("blaztFusionBestrecordsaves"))
            {
                return PlayerPrefs.GetInt("blaztFusionBestrecordsaves");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("blaztFusionBestrecordsaves", value);
        }
    }

    public static int blaztfusiontrycounts
    {
        get
        {
            if (PlayerPrefs.HasKey("blaztfusiontrycountssaves"))
            {
                return PlayerPrefs.GetInt("blaztfusiontrycountssaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("blaztfusiontrycountssaves", value);
        }
    }

    public static string blaztfusionname;

    public static int blaztfusionwinscont
    {
        get
        {
            if (PlayerPrefs.HasKey("blaztfusionwinscontSave"))
            {
                return PlayerPrefs.GetInt("blaztfusionwinscontSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("blaztfusionwinscontSave", value);
        }
    }

    public static Sprite targetBerry;

    [SerializeField]
    private blaztTube[] _blaztTubes;

    [SerializeField]
    private Image _displayTargetBerry;

    [SerializeField]
    private TMP_Text[] _displayScores;

    [SerializeField]
    private TMP_Text _displayTime;

    [SerializeField]
    private GameObject _resultPage;

    public static int currentScore;

    public static UnityEvent setNewBerry = new UnityEvent();

    private float _time;

    private int _reachedCount;

    private void Start()
    {
        _time = 10;
        _reachedCount = 0;
        currentScore = 0;
        setNewBerry.AddListener(SetNewTargetBerry);
        SetNewTargetBerry();
    }

    private void LateUpdate()
    {
        foreach (var item in _displayScores)
        {
            item.text = currentScore.ToString();
        }
        if (currentScore > blaztfusionBestRecordCount)
        {
            blaztfusionBestRecordCount = currentScore;   
        }
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            _resultPage.SetActive(true);
            return;
        }

        _displayTime.text = _time.ToString("0.0") + "s";
    }

    private void SetNewTargetBerry()
    {
        foreach (var item in _blaztTubes)
        {
            item.DestroyFirst();
            item.SpawnNew();
        }
        List<Image> currentBerryes = new List<Image>();
        foreach (var item in _blaztTubes)
        {
            currentBerryes.Add(item.GetFirstImage());
        }
        targetBerry = currentBerryes[Random.Range(0, currentBerryes.Count)].sprite;
        _reachedCount +=1;
        _time += 1 - (1 * (0.06f * _reachedCount));
        currentScore += Random.Range(1, 20);
        _displayTargetBerry.sprite = targetBerry;
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
