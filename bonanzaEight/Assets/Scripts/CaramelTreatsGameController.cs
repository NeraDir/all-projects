using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaramelTreatsGameController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _bgSprites;

    [SerializeField]
    private Image[] _bgImages;

    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private Transform _targetSpawnPosition;

    [SerializeField]
    private Image _caramelPrefab; 

    [SerializeField]
    private CaramelDatas _caramelDatas;

    [SerializeField]
    private Joystick _joystick;

    [SerializeField]
    private Image _targetCaramelImage;

    [SerializeField]
    private Transform _starsTransform;

    [SerializeField]
    private TMP_Text[] _starsShow;

    [SerializeField]
    private TMP_Text _timerShow;

    [SerializeField]
    private TMP_Text[] _levelShow;

    [SerializeField]
    private TMP_Text _levelPassStatus;

    [SerializeField]
    private GameObject _starPref;

    [SerializeField]
    private GameObject _resultScreen;

    [SerializeField]
    private GameObject _nextButton;

    public static Sprite targetCaramel;

    public static int MaxReachLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("CaramelTreatsMaxReachLevelKey"))
                return PlayerPrefs.GetInt("CaramelTreatsMaxReachLevelKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("CaramelTreatsMaxReachLevelKey", value);
        }
    }

    public static int SelectedBgIndex
    {
        get
        {
            if(PlayerPrefs.HasKey("CaramelTreatsSelectedBgIndexKey"))
                return PlayerPrefs.GetInt("CaramelTreatsSelectedBgIndexKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("CaramelTreatsSelectedBgIndexKey", value);
        }
    }

    public static int EarnedStarsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("CaramelTreatsEarnedStarsCountKey"))
                return PlayerPrefs.GetInt("CaramelTreatsEarnedStarsCountKey");
            return 1000;
        }
        set
        {
            PlayerPrefs.SetInt("CaramelTreatsEarnedStarsCountKey", value);
        }
    }

    private int CurrentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("CaramelTreatsCurrentLevelSaveKey"))
            {
                return PlayerPrefs.GetInt("CaramelTreatsCurrentLevelSaveKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("CaramelTreatsCurrentLevelSaveKey", value);
        }
    }

    public static UnityEvent onUpMouse = new UnityEvent();
    public static UnityEvent<GameObject> onUpAddToList = new UnityEvent<GameObject>();
    public static UnityEvent<GameObject> onStarGet = new UnityEvent<GameObject>();
    public static UnityEvent<Vector3,int> onSpawnNewCaramel = new UnityEvent<Vector3, int>();

    public static List<GameObject> caramelsInJar = new List<GameObject>();

    public List<GameObject> caramelsTemp = new List<GameObject>();

    public static bool go;

    private float _timer;

    private void Start()
    {
        foreach (var item in _bgImages)
        {
            item.sprite = _bgSprites[SelectedBgIndex];
        }
        caramelsInJar.Clear();
        go = false;
        _timer = 180;
        onUpMouse.AddListener(OnSpawnNewCandy);
        onUpAddToList.AddListener(OnAdd);
        onStarGet.AddListener(GetStar);
        onSpawnNewCaramel.AddListener(OnSpawn);
        OnFillJar();
    }

    private void GetStar(GameObject starTemp)
    {
        starTemp.transform.DOMove(_starsTransform.position, 0.25f).OnComplete(() => starTemp.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(starTemp.gameObject); EarnedStarsCount += Random.Range(1,5); }));
    }

    private void OnAdd(GameObject gem)
    {
        caramelsInJar.Add(gem);
    }

    private void LateUpdate()
    {
        if (caramelsInJar.Count <= 0)
            return;
        foreach (var item in caramelsInJar)
        {
            if(item == null)
                caramelsInJar.Remove(item);
        }
        caramelsTemp = caramelsInJar;
        if (_timer <= 0 && caramelsInJar.Count > 0)
        {
            _nextButton.SetActive(false);
            _levelPassStatus.text = "LEVEL NOT PASSED";
            _resultScreen.SetActive(true);
            go = true;
            return;
        }
        if (caramelsInJar.Count <= 0 && _timer > 0)
        {
            _nextButton.SetActive(true);
            _levelPassStatus.text = "LEVEL PASSED";
            _resultScreen.SetActive(true);
            go = true;
            return;
        }
        foreach (var item in _starsShow)
        {
            item.text = "x" + EarnedStarsCount.ToString();
        }
        foreach (var item in _levelShow)
        {
            item.text = CurrentLevel.ToString("0");
        }
        _timer -= Time.deltaTime;
        _timerShow.text = _timer.ToString("0.0") + "s";
    }

    private void OnDestroy()
    {
        onUpMouse.RemoveListener(OnSpawnNewCandy);
        onUpAddToList.RemoveListener(OnAdd);
        onStarGet.RemoveListener(GetStar);
        onSpawnNewCaramel.RemoveListener(OnSpawn);
    }

    private void OnSpawnNewCandy()
    {
        StartCoroutine(OnUp());
    }

    private IEnumerator OnUp()
    {
        yield return new WaitForSeconds(0.5f);
        Image tempCandy = Instantiate(_caramelPrefab, _targetSpawnPosition.position, Quaternion.Euler(0, 0, Random.Range(-360, 360)), _targetSpawnPosition.parent);
        float rndScale = Random.Range(0.4f, 0.9f);
        tempCandy.transform.localScale = new Vector3(rndScale, rndScale, rndScale);
        tempCandy.sprite = _caramelDatas.caramelSprites[0];
        tempCandy.transform.SetSiblingIndex(1);
        tempCandy.GetComponent<CaramelCandyComponent>().Init(_joystick);
    }

    private void OnSpawn(Vector3 tr,int index)
    {
        int maxIndex = _caramelDatas.caramelSprites.IndexOf(targetCaramel);
        Image tempCandy = Instantiate(_caramelPrefab, tr, Quaternion.Euler(0, 0, Random.Range(-360, 360)), _spawnPositions[0].parent.parent);
        tempCandy.sprite = _caramelDatas.caramelSprites[index + 1 >= maxIndex + 1 ? index : index + 1];
        tempCandy.transform.SetSiblingIndex(1);
        tempCandy.GetComponent<CaramelCandyComponent>().isTriggered = false;
        float rndScale = Random.Range(0.4f, 0.9f);
        tempCandy.transform.localScale = new Vector3(rndScale, rndScale, rndScale);
        GameObject tempStar = Instantiate(_starPref, tr, Quaternion.Euler(0, 0, Random.Range(-360, 360)), _spawnPositions[0].parent.parent);
        GetStar(tempStar);
        caramelsInJar.Add(tempCandy.gameObject);
    }

    private void OnFillJar()
    {
        targetCaramel = _caramelDatas.caramelSprites[Random.Range(0, _caramelDatas.caramelSprites.Count)];
        int maxIndex = _caramelDatas.caramelSprites.IndexOf(targetCaramel);
        for (int i = 0; i < (CurrentLevel + 3 >= 36 ? 36:CurrentLevel + 3); i++)
        {
            Image tempCandy = Instantiate(_caramelPrefab, new Vector3(Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x), _spawnPositions[0].position.y, _spawnPositions[0].position.z), Quaternion.Euler(0, 0, Random.Range(-360, 360)), _spawnPositions[0].parent.parent);
            tempCandy.sprite = _caramelDatas.caramelSprites[Random.Range(0, maxIndex)];
            tempCandy.transform.SetSiblingIndex(1);
            float rndScale = Random.Range(0.4f, 0.9f);
            tempCandy.transform.localScale = new Vector3(rndScale, rndScale, rndScale);
            caramelsInJar.Add(tempCandy.gameObject);
        }
        _targetCaramelImage.sprite = targetCaramel;
        StartCoroutine(OnUp());
    }

    public void OnClickRestart()
    {
        CurrentLevel = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        CurrentLevel = 1;
        Scene nextScene = SceneManager.CreateScene("CaramelTreatsMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }

    public void OnClickNext()
    {
        CurrentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
