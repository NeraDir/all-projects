using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _levelShow;

    [SerializeField]
    private TMP_Text _timeShow;

    [SerializeField]
    private TMP_Text _countToEarn;

    [SerializeField]
    private GameObject _levelEndPage;

    [SerializeField]
    private TMP_Text _levelEndTxt;

    [SerializeField]
    private Transform[] _fruitsSpawnPositions;

    [SerializeField]
    private Image _fruitPrefab;

    [SerializeField]
    private Image _fruitTargetDisplay;

    [SerializeField]
    private Sprite[] _fruitSprites;

    [SerializeField]
    private Image _basketImage;

    public static int MaxLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaztBlazersMaxLevelSigfdiugdugdiKey"))
            {
                return PlayerPrefs.GetInt("BlaztBlazersMaxLevelSigfdiugdugdiKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("BlaztBlazersMaxLevelSigfdiugdugdiKey", value);
        }
    }

    public static int blaztBlazersTryCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("blaztBlazersTryCountsssioguisdfugdusfgudsufsaves"))
            {
                return PlayerPrefs.GetInt("blaztBlazersTryCountsssioguisdfugdusfgudsufsaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("blaztBlazersTryCountsssioguisdfugdusfgudsufsaves", value);
        }
    }

    public static string blaztBlazersName;

    public static int blaztBlazersWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("blaztBlazersWinsCountsdogisdfgudufsave"))
            {
                return PlayerPrefs.GetInt("blaztBlazersWinsCountsdogisdfgudufsave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("blaztBlazersWinsCountsdogisdfgudufsave", value);
        }
    }


    public static int CurrentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaztBlazersCurrentLevelSigfdiugdugdiKey"))
            {
                return PlayerPrefs.GetInt("BlaztBlazersCurrentLevelSigfdiugdugdiKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("BlaztBlazersCurrentLevelSigfdiugdugdiKey", value);
        }
    }

    public static Sprite TargetFruit;
    public static Image BasketImage;
    public static int CurrentCountFruits;
    public static int NeedCountFruits;

    public static bool _isSimpleMode;
    private List<Image> _fruitimages = new List<Image>();

    private bool _isLaunched;

    private void Start()
    {
        if (Random.Range(0,2) != 0)
        {
            _isSimpleMode = false;
        }
        else
        {
            _isSimpleMode = true;
        }
        if (!_isSimpleMode)
        {
            _fruitTargetDisplay.gameObject.SetActive(true);
            TargetFruit = _fruitSprites[Random.Range(0, _fruitSprites.Length)];
            _fruitTargetDisplay.sprite = TargetFruit;
        }
        _timer = 20;
        BasketImage = _basketImage;
        NeedCountFruits = 0;
        CurrentCountFruits = 0;
        for (int i = 0; i < CurrentLevel; i++)
        {
            NeedCountFruits += 1;
        }
        SpawnNewFruits();

    }

    private float _timer;

    private void LateUpdate()
    {
        if (CurrentLevel > MaxLevel)
        {
            MaxLevel = CurrentLevel;
        }
        if (!_isLaunched)
            return;
        if (CurrentCountFruits >= NeedCountFruits)
        {
            _levelEndPage.SetActive(true);
            _levelEndTxt.text = "YOU WIN!\nLEVEL COMPLETED";
            return;
        }
        if (_timer <= 0)
        {
            _levelEndPage.SetActive(true);
            _levelEndTxt.text = "YOU LOOSE!\nLEVEL NOT COMPLETED";
            return;
        }
        _timer -= Time.deltaTime;
        _timeShow.text = _timer.ToString("0.0") + "s";
        _levelShow.text = "LEVEL " + CurrentLevel.ToString();
        _countToEarn.text = CurrentCountFruits.ToString() +"/" + NeedCountFruits.ToString();
    }

    private void SpawnNewFruits()
    {
        List<Image> currentListOfSpawned = new List<Image>();
        if (_isSimpleMode)
        {
            for (int i = 0; i < NeedCountFruits; i++)
            {
                Image tempfruit = Instantiate(_fruitPrefab, new Vector3(Random.Range(_fruitsSpawnPositions[0].position.x, _fruitsSpawnPositions[1].position.x), Random.Range(_fruitsSpawnPositions[0].position.y, _fruitsSpawnPositions[1].position.y), 0), Quaternion.Euler(0, 0, Random.Range(-360, 360)),_fruitsSpawnPositions[1].parent);
                tempfruit.sprite = _fruitSprites[Random.Range(0,_fruitSprites.Length)];
                tempfruit.transform.SetSiblingIndex(0);
                _fruitimages.Add(tempfruit);
            }
        }
        else
        {
            for (int i = 0; i < NeedCountFruits + 3; i++)
            {
                Image tempfruit = Instantiate(_fruitPrefab, new Vector3(Random.Range(_fruitsSpawnPositions[0].position.x, _fruitsSpawnPositions[1].position.x), Random.Range(_fruitsSpawnPositions[0].position.y, _fruitsSpawnPositions[1].position.y), 0), Quaternion.Euler(0, 0, Random.Range(-360, 360)), _fruitsSpawnPositions[1].parent);
                tempfruit.sprite = _fruitSprites[Random.Range(0, _fruitSprites.Length)];
                tempfruit.transform.SetSiblingIndex(0);
                _fruitimages.Add(tempfruit);
                if (tempfruit.sprite == TargetFruit)
                {
                    currentListOfSpawned.Add(tempfruit);
                }
            }
            if (currentListOfSpawned.Count < NeedCountFruits)
            {
                for (int i = 0; i < NeedCountFruits - currentListOfSpawned.Count; i++)
                {
                    Image tempfruit = Instantiate(_fruitPrefab, new Vector3(Random.Range(_fruitsSpawnPositions[0].position.x, _fruitsSpawnPositions[1].position.x), Random.Range(_fruitsSpawnPositions[0].position.y, _fruitsSpawnPositions[1].position.y), 0), Quaternion.Euler(0, 0, Random.Range(-360, 360)), _fruitsSpawnPositions[1].parent);
                    tempfruit.transform.SetSiblingIndex(0);
                    tempfruit.sprite = TargetFruit;
                }
            }
        }
        _isLaunched = true;
    }

    private void OnApplicationQuit()
    {
        CurrentLevel = 1;
    }

    public void OnClickRestart()
    {
        CurrentLevel = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        CurrentLevel = 1;
        SceneManager.LoadScene("Menu");
    }

    public void OnClickNext()
    {
        CurrentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
