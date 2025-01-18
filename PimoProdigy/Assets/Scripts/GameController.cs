using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static List<CellComponent> selectedCells = new List<CellComponent>();

    public static Action moveCells;
    public static Action win;

    public static bool canMove;

    [SerializeField]
    private TMP_Text _stepsTxt;

    public static int stepsCount;

    public static int levelIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("PimoProdigyLevelIndexKey"))
                return PlayerPrefs.GetInt("PimoProdigyLevelIndexKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PimoProdigyLevelIndexKey", value);
        }
    }

    public static int MaxReachLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("PimoProdigyMaxReachLevelKey"))
            {
                return PlayerPrefs.GetInt("PimoProdigyMaxReachLevelKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PimoProdigyMaxReachLevelKey", value);
        }
    }


    public static Sprite targetSprite;

    private int _stepsCount;

    [SerializeField]
    private Image _targetImage;

    [SerializeField]
    private GameObject[] _levelBoards;

    [SerializeField]
    private List<Sprite> _sprites;

    [SerializeField]
    private TMP_Text _levelTxt;

    [SerializeField]
    private GameObject _winPage;

    [SerializeField]
    private GameObject _loosePage;

    private float timer_;

    [SerializeField]
    private TMP_Text _timer;

    private bool _isEnd;

    private int activeBoard;

    private void Awake()
    {
        GameController.selectedCells.Clear();
        for (int i = 0; i < levelIndex + 1; i++)
        {
            timer_ += 2.5f;
        }
        if (timer_ >= 20)
        {
            timer_ = 20;
        }
        if (levelIndex+1 > _levelBoards.Length)
        {
            activeBoard = Random.Range(0, _levelBoards.Length);
            _levelBoards[activeBoard].SetActive(true);
        }
        else
        {
            activeBoard = levelIndex;
            _levelBoards[activeBoard].SetActive(true);
        }
        _levelTxt.text = "LVL " + (levelIndex + 1).ToString();
        stepsCount = 0;
        moveCells += OnMoveCells;
        win += OnWin;
        targetSprite = _sprites[Random.Range(0, _sprites.Count)];
        _sprites.Remove(targetSprite);
        _targetImage.sprite = targetSprite;
        foreach (var item in _levelBoards[activeBoard].GetComponentsInChildren<CellComponent>())
        {
            if (item.isFirst)
                item.Init(targetSprite);
            if (item.isRock)
                continue;
            if (item.isFinish)
                continue;
            item.Init(_sprites[Random.Range(0, _sprites.Count)]);
        }
        foreach (var item in _levelBoards[activeBoard].GetComponentsInChildren<CellComponent>())
        {
            if (item.isFirst)
                item.Init(targetSprite);
        }
    }

    private void OnDestroy()
    {
        moveCells -= OnMoveCells;
        win -= OnWin;
    }

    private void OnWin()
    {
        _isEnd = true;
        _winPage.SetActive(true);
    }

    private void OnMoveCells()
    {
        Debug.Log(Vector3.Distance(GameController.selectedCells[0].transform.position, GameController.selectedCells[1].transform.position));
        stepsCount += 1;
        List<Vector3> cellsPositions = new List<Vector3>();
        foreach (var item in selectedCells)
        {
            cellsPositions.Add(item.transform.position);
        }
        selectedCells[0].transform.DOScale(1.2f, 0.25f / 2).OnComplete(() => selectedCells[0].transform.DOScale(1f, 0.25f / 2));

        if (!selectedCells[1].isFinish)
        {
            selectedCells[0].transform.DOMove(cellsPositions[1], 0.25f);
            selectedCells[1].transform.DOScale(0.8f, 0.25f / 2).OnComplete(() => selectedCells[1].transform.DOScale(1f, 0.25f / 2));
            selectedCells[1].transform.DOMove(cellsPositions[0], 0.25f).OnComplete(() =>
            {
                foreach (var item in selectedCells)
                {
                    item.OnSelectChange(false);
                }
                selectedCells.Clear();
                GameController.canMove = false;
            });
        }
        else
        {

            selectedCells[0].transform.DOMove(cellsPositions[1], 0.25f).OnComplete(() =>
            {
                foreach (var item in selectedCells)
                {
                    item.OnSelectChange(false);
                }
                selectedCells.Clear();
                GameController.canMove = false;
                OnWin();
            });
        }
    }

    private void LateUpdate()
    {
        if (_isEnd)
            return;
        timer_ -= Time.deltaTime;
        if (timer_ <= 0)
        {
            _loosePage.SetActive(true);
            _isEnd = true;
        }
        _stepsTxt.text = stepsCount.ToString();
        _timer.text = timer_.ToString("0.0") + "s";
    }

    public void OnPressRestart()
    {
        if (levelIndex > MaxReachLevel)
        {
            MaxReachLevel = levelIndex;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPressNext()
    {
        levelIndex += 1;
        if (levelIndex > MaxReachLevel)
        {
            MaxReachLevel = levelIndex;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPressMenu()
    {
        if (levelIndex > MaxReachLevel)
        {
            MaxReachLevel = levelIndex;
        }
        levelIndex = 0;
        Scene nextScene = SceneManager.CreateScene("PimoProdigyMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }
}
