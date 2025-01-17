using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoadGameComponent : MonoBehaviour
{
    public static int RoarGameMaxReachedLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("RoarGameMaxReachedLevelSaveKey"))
            {
                return PlayerPrefs.GetInt("RoarGameMaxReachedLevelSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("RoarGameMaxReachedLevelSaveKey", value);
        }
    }

    public static int RoarGameLaunchesCount
    {
        get
        {
            if (PlayerPrefs.HasKey("RoarGameLaunchesCountSaveKey"))
            {
                return PlayerPrefs.GetInt("RoarGameLaunchesCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("RoarGameLaunchesCountSaveKey", value);
        }
    }

    public static string roarGameInitializationKey;

    public static int RoarGameCanvasMarginValue
    {
        get
        {
            if (PlayerPrefs.HasKey("RoarGameCanvasMarginValueSaveKey"))
            {
                return PlayerPrefs.GetInt("RoarGameCanvasMarginValueSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("RoarGameCanvasMarginValueSaveKey", value);
        }
    }

    [SerializeField]
    private RoarTableManager _roarTableManager;

    [SerializeField]
    private Image _firstCrystallImage;

    [SerializeField]
    private Image _secondCrystallImage;

    [SerializeField]
    private Text _firstCrystallCountTxt;

    [SerializeField]
    private Text _secondCrystallCountTxt;

    [SerializeField]
    private Text _timerTxt;

    [SerializeField]
    private Text[] _levelTxt;

    [SerializeField]
    private GameObject _resultGood;

    [SerializeField]
    private GameObject _resultBad;

    [SerializeField]
    private GameObject _resultPage;

    [SerializeField]
    private Sprite[] _crystallSprites;

    private int _firstNeedCrystallsCount;
    private int _secondNeedCrystallsCount;

    private float _crystallsgameTime;

    private int _crystallCurrentLevel;

    private bool _isClick;

    private void Start()
    {
        _crystallsgameTime = 15f;
        _crystallCurrentLevel = 1;
        _isClick = false;
        SetNewNeedCrystalls();
    }

    private void LateUpdate()
    {
        _crystallsgameTime -= Time.deltaTime;
        if (_crystallCurrentLevel > RoarGameMaxReachedLevel)
        {
            RoarGameMaxReachedLevel = _crystallCurrentLevel;
        }
        _timerTxt.text = _crystallsgameTime.ToString("0") + "s";
        if (_crystallsgameTime <= 0)
        {
            _resultPage.SetActive(true);
        }
        foreach (var item in _levelTxt)
        {
            item.text = "LVL " + _crystallCurrentLevel.ToString("0");
        }
    }

    private void SetNewNeedCrystalls() 
    {
        List<Sprite> crystallsSpriters = new List<Sprite>();
        foreach (var item in _crystallSprites)
        {
            crystallsSpriters.Add(item);
        }
        int index1 = Random.Range(0, crystallsSpriters.Count);
        _firstCrystallImage.sprite = crystallsSpriters[index1];
        crystallsSpriters.Remove(crystallsSpriters[index1]);
        _secondCrystallImage.sprite = crystallsSpriters[Random.Range(0, crystallsSpriters.Count)];
        crystallsSpriters.Remove(crystallsSpriters[Random.Range(0, crystallsSpriters.Count)]);
        _secondNeedCrystallsCount = Random.Range(1, 4);
        _firstNeedCrystallsCount = Random.Range(1, 4);
        _secondCrystallCountTxt.text = "x" + _secondNeedCrystallsCount.ToString("0");
        _firstCrystallCountTxt.text = "x" + _firstNeedCrystallsCount.ToString("0");
    }

    public void OnClickRefill() 
    {
        _roarTableManager.ReFill();
    }

    public void OnClickCheckIn() 
    {
        if (_isClick)
            return; 
        _isClick = true;
        List<Image> images = _roarTableManager.GetImagesToCheckIn().ToList();
        List<Image> firstImages = images.FindAll(x => x.sprite == _firstCrystallImage.sprite);
        List<Image> secondImages = images.FindAll(x => x.sprite == _secondCrystallImage.sprite);
        if (secondImages.Count > 0)
        {
            if (secondImages.Count >= _secondNeedCrystallsCount)
            {
                Debug.Log("Good Second");
            }
            else
            {
                _resultBad.SetActive(true);
                Invoke(nameof(Invoker), 0.5f);
                return;
            }
            if (firstImages.Count > 0)
            {
                if (firstImages.Count >= _firstNeedCrystallsCount)
                {
                    Debug.Log("Good First");
                    _resultGood.SetActive(true);
                    _crystallsgameTime = 15f;
                    _crystallCurrentLevel++;
                    OnClickRefill();
                    SetNewNeedCrystalls();
                    Invoke(nameof(Invoker), 0.5f);
                }
                else
                {
                    _resultBad.SetActive(true);
                    Invoke(nameof(Invoker), 0.5f);
                    return;
                }
            }
            else
            {
                _resultBad.SetActive(true);
                Invoke(nameof(Invoker), 0.5f);
                return;
            }
        }
        else
        {
            _resultBad.SetActive(true);
            Invoke(nameof(Invoker), 0.5f);
            return;
        }
    }

    private void Invoker() 
    {
        _isClick = false;
    }

    public void OnClickRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu() 
    {
        SceneManager.LoadScene("RoarMenuScene");
    }
}
