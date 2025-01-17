using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
public class MainMenager : MonoBehaviour
{
    [SerializeField] private GameObject _treePrefab;
    [SerializeField] private float _startSpeed = 1;
    [SerializeField] private float _acceleration = 1;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private TextMeshProUGUI _petrolTexte;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private float petro = 100;

    private float timerReset = 2.5f;
    private float timer = 0;
    public float speed => _speed;
    private float _speed;
    public static MainMenager instance;

    private void Awake()
    {
        instance = this;
        timer = timerReset;
    }

    private void Start()
    {
        _speed = _startSpeed;
        _mainText.text = "";
        _restartButton.SetActive(false);
    }
    void FixedUpdate()
    {
        _speed = _speed + _acceleration * Time.deltaTime;
        if (timer <= 0)
        {
            timer = timerReset;
            float x = UnityEngine.Random.RandomRange(-1.5001f, 1.5001f);
            Instantiate(_treePrefab, new Vector3(x, 0.46f, 10.6f), _treePrefab.transform.rotation);
        }
        timer -= Time.fixedDeltaTime;
        petro -= Time.fixedDeltaTime;
        _petrolTexte.text = "petro = " + Math.Round(petro, 2).ToString();
    }
    public void Stop()
    {
        Time.timeScale = 0;
        _mainText.text = "petro = " + Math.Round(petro, 2).ToString();
        _restartButton.SetActive(true);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _restartButton.SetActive(false);
    }
}
