using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelResulter : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainHero;
    [SerializeField]
    private GameObject _winPanel;
    [SerializeField]
    private DiliveryCounter _diliveryCounter;
    [SerializeField]
    private LevelTimer _levelTimer;
    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private int _moneyForGame;
    [SerializeField]
    private Text _moneyForGameText;
    [SerializeField]
    private Button _menuButton;
    [SerializeField]
    private GameObject[] _restartPosition;

    public void Start()
    {
        Time.timeScale = 1f;
    }

    public void LevelWin()
    {
        _moneyForGameText.text = $"x{_moneyForGame}";
        _menuButton.interactable = false;
        _joystick.gameObject.SetActive(false);
        _winPanel.SetActive(true);
        _levelTimer.enabled = false;
        MoneyCounter.ReceiveMoney(_moneyForGame);
        Time.timeScale = 0f;
    }

    public void LevelRestart()
    {
        //_levelTimer.enabled = true;
        //_menuButton.interactable = true;
        //_winPanel.SetActive(false);
        SetStartPosition();

        _diliveryCounter.ResetDiliveryCount();

    }

    public void SetStartPosition()
    {
        int randomPlaceIndex = Random.Range(0, _restartPosition.Length);

        _mainHero.transform.position = _restartPosition[randomPlaceIndex].transform.position;

        _mainHero.transform.rotation = _restartPosition[randomPlaceIndex].transform.rotation;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }
}
