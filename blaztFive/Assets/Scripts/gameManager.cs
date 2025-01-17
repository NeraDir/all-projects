using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnedFruit
{
    public Sprite fruitSprite;
    public int fruitCount;
}

public class gameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] _showCurrentLevel;

    [SerializeField]
    private Animator _preGameAniamtor;

    [SerializeField]
    private GameObject _resultPage;

    [SerializeField]
    private TMP_Text _showResultTxt;

    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private Image _tempFruit;

    [SerializeField]
    private GameObject _resultButton;

    [SerializeField]
    private GameObject _checkingPage;

    private bool _isClicked;

    [SerializeField]
    private Sprite[] _fruitSprites;

    [SerializeField]
    private checkingmanager[] _checkingManagers;

    public static List<Sprite> fruitsToCheckingList = new List<Sprite>();

    public static List<SpawnedFruit> spawnedFruits = new List<SpawnedFruit>();

    public static int CurrentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaseFogoSiufgdgudifCurrentLevelKey"))
            {
                return PlayerPrefs.GetInt("BlaseFogoSiufgdgudifCurrentLevelKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("BlaseFogoSiufgdgudifCurrentLevelKey", value);
        }
    }

    public static int MaxLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaseFogoSiufgdgudifMaxLevelKey"))
            {
                return PlayerPrefs.GetInt("BlaseFogoSiufgdgudifMaxLevelKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("BlaseFogoSiufgdgudifMaxLevelKey", value);
        }
    }

    private void Awake()
    {
        spawnedFruits.Clear();
        fruitsToCheckingList.Clear();
        fruitsToCheckingList = _fruitSprites.ToList();
    }

    private void LateUpdate()
    {
        foreach (var item in _showCurrentLevel)
        {
            item.text = "LEVEL " + CurrentLevel.ToString();
        }

        if (CurrentLevel > MaxLevel)
        {
            MaxLevel = CurrentLevel;
        }
    }

    private IEnumerator SpawnNewFruits()
    {
        int rndCount = CurrentLevel + 1;
        for (int i = 0; i < rndCount; i++)
        {
            Image tempObject = Instantiate(_tempFruit, new Vector3(Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x), _spawnPositions[0].position.y, 0), Quaternion.Euler(0, 0, Random.Range(-360, 360)), _spawnPositions[0].parent);
            tempObject.transform.SetSiblingIndex(0);
            float scale = Random.Range(0.7f, 1.3f);
            tempObject.sprite = fruitsToCheckingList[Random.Range(0, fruitsToCheckingList.Count)];
            tempObject.transform.localScale = new Vector3(scale, scale, scale);
            SpawnedFruit spawnedFruittemp = new SpawnedFruit();
            spawnedFruittemp.fruitSprite = tempObject.sprite;
            spawnedFruittemp.fruitCount += 1;
            SpawnedFruit tempCheckfruit = spawnedFruits.Find(x => x.fruitSprite == spawnedFruittemp.fruitSprite);
            if (tempCheckfruit != null)
            {
                tempCheckfruit.fruitCount += 1;
            }
            else
            {
                spawnedFruits.Add(spawnedFruittemp);
            }
            yield return new WaitForSeconds(0.4f);
        }
        for (int i = 0; i < _checkingManagers.Length; i++)
        {
            int rndSprite = Random.Range(0, fruitsToCheckingList.Count);
            _checkingManagers[i].GetComponent<Image>().sprite = fruitsToCheckingList[rndSprite];
            fruitsToCheckingList.Remove(fruitsToCheckingList[rndSprite]);
        }
        yield return new WaitForSeconds(2);
        _checkingPage.SetActive(true);
    }

    public void OnClickCheck()
    {
        if (_checkingManagers[2].GetState() && _checkingManagers[1].GetState() && _checkingManagers[0].GetState())
        {
            _resultPage.SetActive(true);
            _resultButton.SetActive(true);
            _showResultTxt.text = "LEVEL PASSED";
            return;
        }
        _resultPage.SetActive(true);
        _resultButton.SetActive(false);
        _showResultTxt.text = "LEVEL NOT PASSED";
    }

    public void OnClickStartGame()
    {
        if (_isClicked) return;
        _isClicked = true;
        _preGameAniamtor.SetBool("ui_panels_state", true);
        StartCoroutine(ClosePage(_preGameAniamtor.gameObject,0.5f));
    }

    private IEnumerator ClosePage(GameObject page,float time)
    {
        yield return new WaitForSeconds(time);
        page.SetActive(false);
        _isClicked = false;
        StartCoroutine(SpawnNewFruits());


    }

    private void OnApplicationQuit()
    {
        CurrentLevel = 1;
    }

    public void OnClickNext()
    {
        CurrentLevel += 1;
        SceneManager.LoadScene("Game");
    }

    public void OnClickRestart()
    {
        CurrentLevel  = 1;
        SceneManager.LoadScene("Game");
    }

    public void OnClickMenu()
    {
        CurrentLevel = 1;
        SceneManager.LoadScene("MainScene");
    }
}
