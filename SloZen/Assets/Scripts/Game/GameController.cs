using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static int CurrentLevel
    {
        get => PlayerPrefs.GetInt("SloZenMusicCurrentLevelSaveKey", 0);
        set => PlayerPrefs.SetInt("SloZenMusicCurrentLevelSaveKey", value);
    }

    public static PackComponent CurrentPack;

    [SerializeField] private LevelSettings _levelSettings;

    [SerializeField] private PackComponent _tempBoxes;

    [SerializeField] private GameObject _block;

    [SerializeField] private Transform _parentOfFruits;

    [SerializeField] private List<PackComponent> _packsPrefabs;
    [SerializeField] private Transform _packsSpawnPosition;
    [SerializeField] private List<FruitItemComponent> _fruitPrefabs;
    [SerializeField] private Transform[] _fruitBlocks;

    [SerializeField] private float _distanceFruit;

    [SerializeField] private float _distancePacks;

    public static List<FruitItemComponent> curreFruitsList = new List<FruitItemComponent>();
    public static float spawnedFruits;
    public static float destroyedFruits;

    [SerializeField] private GameObject _resultParent;
    [SerializeField] private ResultWindow _resultWindow;
    [SerializeField] private PauseWindow _pauseWindow;

    [SerializeField] private Text currentLevelTxt;
    [SerializeField] private Slider _progress;

    private bool isLaunched;

    private void Awake()
    {
        if (CurrentLevel >= _levelSettings.levelDatas.Length)
        {
            CurrentLevel = Random.Range(0, _levelSettings.levelDatas.Length);
        }
        destroyedFruits = 0;
        spawnedFruits = 0;
        CurrentPack = null;
        curreFruitsList.Clear();
        currentLevelTxt.text = "LEVEL " + (CurrentLevel + 1).ToString();
        SetupFruits();
        SetupPacks();
        isLaunched = true;
    }

    private void LateUpdate()
    {
        if (!isLaunched)
            return;
        if (curreFruitsList.Count <= 0)
        {
            _resultParent.gameObject.SetActive(true);
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (CurrentPack == null)
                    return;
                if (hit.transform.TryGetComponent(out FruitItemComponent fruit))
                {
                    if (fruit.fruitType == CurrentPack.fruitType)
                    {
                        if (fruit.fruitType == CurrentPack.fruitType)
                        {
                            Destroy(fruit.GetComponent<Rigidbody>());
                            Destroy(fruit.GetComponent<Collider>());
                            CurrentPack.AddFruit(fruit);
                        }
                        else
                        {
                            Destroy(fruit.GetComponent<Rigidbody>());
                            _tempBoxes.AddFruit(fruit);
                        }
                    }
                }
            }
        }

        if(CurrentPack != null)
        {
            if (CurrentPack._fruitsList.Count >= 3)
            {
                _block.SetActive(false);
            }
            else
            {
                _block.SetActive(true);
            }
        }
        else
        {
            _block.SetActive(true);
        }

        _progress.value = Mathf.Lerp(_progress.value, destroyedFruits / spawnedFruits, 8 * Time.deltaTime);
    }

    private void SetupPacks()
    {
        PackComponent lastPack = null;
        foreach (var item in _levelSettings.levelDatas[CurrentLevel].fruitsPerLevel)
        {
            PackComponent newPack = _packsPrefabs.Find(x => x.fruitType == item);
            if (newPack != null)
                    if (lastPack != null)
                        lastPack = Instantiate(newPack, new Vector3(lastPack.transform.position.x - _distancePacks, _packsSpawnPosition.position.y, _packsSpawnPosition.position.z), Quaternion.identity);
                    else
                        lastPack = Instantiate(newPack, new Vector3(_packsSpawnPosition.position.x - _distancePacks, _packsSpawnPosition.position.y, _packsSpawnPosition.position.z), Quaternion.identity);

        }
    }

    private void SetupFruits()
    {
        FruitItemComponent lastFruit = null;
        foreach (var item in _levelSettings.levelDatas[CurrentLevel].fruitsPerLevel)
        {
            FruitItemComponent newFruit = _fruitPrefabs.Find(x => x.fruitType == item);
            if (newFruit != null)
                for (int i = 0; i < 3; i++)
                {
                    if (lastFruit != null)
                        lastFruit = Instantiate(newFruit, new Vector3(Random.Range(_fruitBlocks[0].position.x, _fruitBlocks[1].position.x), _fruitBlocks[0].position.y, lastFruit.transform.position.z - _distanceFruit), Quaternion.identity);
                    else
                        lastFruit = Instantiate(newFruit, new Vector3(Random.Range(_fruitBlocks[0].position.x, _fruitBlocks[1].position.x), _fruitBlocks[0].position.y, _fruitBlocks[0].position.z - _distanceFruit), Quaternion.identity);
                    curreFruitsList.Add(lastFruit);
                }
        }
        spawnedFruits = curreFruitsList.Count;
    }
}
