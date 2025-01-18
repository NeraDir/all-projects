using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RaundMenager : MonoBehaviour
{
    [SerializeField] private Sprite[] _playerSprites;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _textEnding;
    [SerializeField] private int _minGold;
    [SerializeField] private int _maxGold;

    private int dir;

    public static RaundMenager istance;
    
    private void Awake()
    {
        _gameOverPanel.SetActive(false);
        if (istance != null)
        {
            Debug.LogWarning("RaundMenager.istance != null");
        }
        istance = this;

        _player.GetComponent<SpriteRenderer>().sprite = _playerSprites[PlayerPrefs.GetInt("skeen")];

    }
    private void Start()
    {
        dir = -1;
        SpawnBall();
    }
    public void GameOver(bool win)
    {
        Time.timeScale = 0;
        //_gameOverPanel.SetActive(true);
        if (win)
        {
            PlayerPrefs.SetInt("minGold", _minGold);
            PlayerPrefs.SetInt("maxGold", _maxGold);
            Time.timeScale = 1;
            if(SceneManager.GetActiveScene().name == "Level_" + PlayerPrefs.GetInt("lvlsOpend").ToString())
            {
                PlayerPrefs.SetInt("lvlsOpend", PlayerPrefs.GetInt("lvlsOpend") + 1);
            }
            SceneManager.LoadScene("Win");
        }
        else
        {
            _textEnding.text = "Lose";
            _gameOverPanel.SetActive(true);
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;
        _gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void SpawnBall()
    {
        GameObject g = Instantiate(_ballPrefab, Vector2.zero, _ballPrefab.transform.rotation);
        if(dir < 0)
        {
            g.GetComponent<Ball>().Jump(Vector2.down, 500);
        }
        else
        {
            g.GetComponent<Ball>().Jump(Vector2.up, 500);
        }
        dir *= 1;
    }
}
