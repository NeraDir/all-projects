using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text _statusTxt;
    [SerializeField] private Text[] _roundTxt;
    [SerializeField] private GameObject _resultScreen;
    [SerializeField] private Text _timerTxt;
    [SerializeField] private MazeSpawner _spawner;

    public static int currentCount;
    public static int _targetCount;
    private int _currentRound
    {
        get => PlayerPrefs.GetInt("GameRoundSaveKey", 1);
        set => PlayerPrefs.SetInt("GameRoundSaveKey", value);
    }

    private void Awake()
    {
        currentCount = 0;
        _targetCount = 0;
        int value = 2;
        for (int i = 0; i < _currentRound; i++)
        {
            value += 2;
            _spawner.Rows = value;
            _spawner.Columns = value;
        }
       
    }

    private void LateUpdate()
    {
        _statusTxt.text = currentCount.ToString() + "/" + _targetCount.ToString();
    }
}
