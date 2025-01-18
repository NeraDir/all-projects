using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameResulter : MonoBehaviour
{
    [SerializeField]
    private Joystick _floatingJoystick;
    [SerializeField]
    private MoneyCounter _moneyCounter;
    [SerializeField]
    private PlaneMovement _planeMovement;
    [SerializeField]
    private Text _moneyCountText;
    [SerializeField]
    private Text _distanceText;
    [SerializeField]
    private GameObject _resultPanel;
    [SerializeField]
    private List<GameObject> _gameEnviroment;

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _gameEnviroment.Add(transform.GetChild(i).gameObject);
        }
    }

    public void GameFailed()
    {
        for (int i = 0; i < _gameEnviroment.Count; i++)
        {
            _gameEnviroment[i].SetActive(false);
        }

        RedrawResultWindow();
        _resultPanel.SetActive(true);
    }

    public void GameRestart()
    {
        MoneyCounter._moneyForGame = 0;
        SceneManager.LoadScene(0);
    }

    public void RedrawResultWindow()
    {
        _moneyCountText.text = MoneyCounter._moneyForGame.ToString();
        _distanceText.text = $"{Mathf.RoundToInt(_planeMovement._currentPoints).ToString()} m";
    }
}
