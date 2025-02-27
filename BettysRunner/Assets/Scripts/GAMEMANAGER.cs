using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GAMEMANAGER : MonoBehaviour
{
    public static Action<int, bool> finished;
    public static bool runLaunched;

    [SerializeField] private Transform _player;

    [SerializeField] private GameObject _prepareScreen;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private PLAYERSSETUPERCOMPONENT _playerSetup;
    [SerializeField] private CAMERAMOVEMENT _cam;
    [SerializeField] private GameObject _results;
    [SerializeField] private Text _resultsText;
    [SerializeField] private Text _placeTxt;
    [SerializeField] private Text _getTxt;

    [SerializeField] private Text _prepareTxt;

    private float _timer = 3;

    private bool _isPrepare;

    private IEnumerator Start()
    {
        _isPrepare = false;
        runLaunched = false;
        finished += OnFinished;
        _timer = 3;
        _prepareTxt.text = "WAITING PLAYERS";
        _playerSetup.Init(_player);
        yield return new WaitForSeconds(15f);
        _prepareTxt.text = "WAITING BETS";
        yield return new WaitForSeconds(5f);
        _isPrepare = true;
        UICUSTOMBUTTONCOMPONENT.buttonClicked = false;
    }

    private void OnDestroy()
    {
        finished -= OnFinished;
    }

    private void OnFinished(int index,bool dead)
    {
        if (dead)
        {
            _results.SetActive(true);
            _getTxt.text = "";
            _placeTxt.text ="";
            _resultsText.text = "YOU LOOSE";
        }
        else
        {
            _results.SetActive(true);
            _getTxt.text = "YOU GET " + (index * 10).ToString();
            _placeTxt.text = "YOUR PLACE - " + index.ToString();
            _resultsText.text = "VICTORY";
            PLAYERDATA.COINS += (index * 10);
        }
    }

    private void LateUpdate()
    {
        if (_isPrepare)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _prepareScreen.SetActive(false);
                _cam._target = _player;
                runLaunched = true;
                _isPrepare = false;
            }
        }
    }
}
