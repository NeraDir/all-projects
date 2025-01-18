using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endgamecontroller : MonoBehaviour
{
    public static int endgamecontrollercanvassizevalue
    {
        get
        {
            if (PlayerPrefs.HasKey("endgamecontrollercanvassizevaluesavekey"))
            {
                return PlayerPrefs.GetInt("endgamecontrollercanvassizevaluesavekey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("endgamecontrollercanvassizevaluesavekey", value);
        }
    }

    public static string endgamesettingskeys;

    public static int endgamecontrollerlaunchCount
    {
        get
        {
            if (PlayerPrefs.HasKey("endgamecontrollerlaunchCountsavekey"))
            {
                return PlayerPrefs.GetInt("endgamecontrollerlaunchCountsavekey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("endgamecontrollerlaunchCountsavekey", value);
        }
    }

    [SerializeField]
    private Image _ballPrefab;

    [SerializeField]
    private Transform[] _ballsSpawnPositions;

    [SerializeField]
    private Text _resultXTxt;

    public static int _resultX;

    [SerializeField]
    private Text _starsCountTxt;

    [SerializeField]
    private Text _distanceTxt;

    [SerializeField]
    private List<ballfallXcom> _ballfallXcoms = new List<ballfallXcom>();

    [SerializeField]
    private GameObject _resultScreen;

    private bool _isStarted;

    private int _resulter;

    private void Start()
    {
        for (int i = 0; i < gamecontoller.ballsCount; i++)
        {
            Image tempBall = Instantiate(_ballPrefab, new Vector3(Random.Range(_ballsSpawnPositions[0].position.x, _ballsSpawnPositions[1].position.x), _ballsSpawnPositions[0].position.y, _ballsSpawnPositions[0].position.z), Quaternion.identity, _ballsSpawnPositions[0].parent);
            tempBall.transform.SetSiblingIndex(0);
        }
        _resulter = 0;
        _isStarted = true;
    }

    private void LateUpdate()
    {
        if (!_isStarted)
            return;
        var ballsListers = _ballfallXcoms.OrderByDescending(x => x.ballsIncase);
        _resultX = ballsListers.ElementAt(0).X;
        _resultXTxt.text = _resultX.ToString("0");
        if (FindObjectOfType<ballfallcom>() == null)
        {
            _resultScreen.SetActive(true);
        }

            _distanceTxt.text = gamecontoller.currentDistance.ToString("0.0") + "m";

            _starsCountTxt.text = (gamecontoller.starsCount * _resultX).ToString("0") + "c";
    }

    public void OnClickRestart() 
    {
        gamecontoller.ballStars += (gamecontoller.starsCount * _resultX);
        SceneManager.LoadScene("game");
    }

    public void OnClickMenu() 
    {
        gamecontoller.ballStars += (gamecontoller.starsCount * _resultX);
        SceneManager.LoadScene("menu");
    }
}
