using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject[] _levels;
    
    [SerializeField] private Sprite _wallSprite;
    [SerializeField] private Sprite _sawSprite;
    [SerializeField] private Sprite _finishSprite;
    [SerializeField] private Sprite _buttonSprite;
    [SerializeField] private Sprite _ballSprite;

    [SerializeField] private TMP_Text _levelTxt;
    [SerializeField] private TMP_Text _timeTxt;
    [SerializeField] private GameObject _winPage;
    [SerializeField] private GameObject _losePage;  
    private GameObject _cellsBoard;

    private BoardItemComponent _lastButton;
    private float _time;
    public static bool isEnd;
    private AudioClip _looseClip;
    private AudioClip _winClip;

    public static int CurrentBgIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("CurrentBGIndexLlinoRimsSaveKey"))
                return PlayerPrefs.GetInt("CurrentBGIndexLlinoRimsSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("CurrentBGIndexLlinoRimsSaveKey", value);
        }
    }
    
    public static int CurrentLevelIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("CurrentLevelLlinoRimsSaveKey"))
                return PlayerPrefs.GetInt("CurrentLevelLlinoRimsSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("CurrentLevelLlinoRimsSaveKey", value);
        }
    }
    
    public static int MaxReachLevelIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("MaxReachLevelLlinoRimsSaveKey"))
                return PlayerPrefs.GetInt("MaxReachLevelLlinoRimsSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("MaxReachLevelLlinoRimsSaveKey", value);
        }
    }

    private List<Transform> _cells = new List<Transform>();

    private IEnumerator Start()
    {
        isEnd = false;
        _looseClip = Resources.Load<AudioClip>("Sounds/Loose");
        _winClip = Resources.Load<AudioClip>("Sounds/Win");
        for (int i = 0; i < CurrentLevelIndex + 1; i++)
        {
            _time += 2.5f;
        }
        _levelTxt.text = "Level " + (CurrentLevelIndex + 1).ToString();
        GetCells();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(AnimatingCells());
        FillCells();
        BallComponent.endBallLive += OnBallEnd;
    }

    private void OnDestroy()
    {
        BallComponent.endBallLive -= OnBallEnd;
    }

    private void OnBallEnd(bool isWin)
    {
        _winPage.SetActive(isWin);
        _losePage.SetActive(!isWin);
        isEnd = true;
    }

    private void LateUpdate()
    {
        if(isEnd)
            return; 
        _time -= Time.deltaTime;
        _timeTxt.text = _time.ToString("0.00")+"s";
        if (_time <= 0)
        {
            isEnd = true;
            OnBallEnd(false);
        }
    }

    private IEnumerator AnimatingCells()
    {
        while (true)
        {
            foreach (var item in _cells)
            {
                item.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.15f).OnComplete(() => item.DOScale(new Vector3(1f, 1f, 1f), 0.15f));
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private void FillCells()
    {
        foreach (var item in _cells)
        {
            CellComponent tempCell = item.GetComponent<CellComponent>();
            if (tempCell != null)
            {
                if (tempCell.CellType != CellType.Nothing)
                {
                    Image tempIamge = Instantiate(_target.GetComponent<Image>(), item.transform.position, item.transform.rotation, _cellsBoard.transform.parent);
                    switch (tempCell.CellType)
                    {
                        case CellType.Ball:
                            tempIamge.GetComponent<BoardItemComponent>().cellType = CellType.Ball;
                            tempIamge.sprite = _ballSprite;
                            tempIamge.gameObject.AddComponent<BallComponent>();
                            tempIamge.GetComponent<CircleCollider2D>().isTrigger = true;
                            tempIamge.gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                            break;
                        case CellType.Saw:
                            tempIamge.sprite = _sawSprite;
                            tempIamge.GetComponent<BoardItemComponent>().cellType = CellType.Saw;
                            break;
                        case CellType.Button:
                            tempIamge.sprite = _buttonSprite;
                            tempIamge.transform.SetSiblingIndex(FindObjectOfType<BallComponent>().transform.GetSiblingIndex() - 1);
                            tempIamge.GetComponent<BoardItemComponent>().cellType = CellType.Button;
                            _lastButton = tempIamge.GetComponent<BoardItemComponent>();
                            break;
                        case CellType.Finish:
                            tempIamge.sprite = _finishSprite;
                            tempIamge.GetComponent<BoardItemComponent>().cellType = CellType.Finish;
                            break;
                        case CellType.Wall:
                            tempIamge.sprite = _wallSprite;
                            break;
                        case CellType.Door:
                            tempIamge.sprite = _wallSprite;
                            tempIamge.GetComponent<BoardItemComponent>().cellType = CellType.Door;
                            _lastButton.AddDoor(tempIamge.transform,item.GetComponent<CellComponent>()); 
                            break;  
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void GetCells()
    {
        if(CurrentLevelIndex > _levels.Length - 1)
            _cellsBoard = _levels[Random.Range(0, _levels.Length)];
        else
            _cellsBoard = _levels[CurrentLevelIndex];
        _cellsBoard.SetActive(true);
        foreach (CellComponent item in _cellsBoard.GetComponentsInChildren<CellComponent>())
        {
            _cells.Add(item.transform);
        }
    }
}
