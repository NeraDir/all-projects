using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [SerializeField]
    private LevelResulter _levelResulter;
    [SerializeField]
    private float _allLevelTime;
    [SerializeField]
    private Text _timerText;

    private int _currentTime;
    private float _currentLevelTime;

    private void OnEnable()
    {
        _currentLevelTime = _allLevelTime;
    }

    void Update()
    {
        if(_currentLevelTime > 0)
        {
            _currentLevelTime -= Time.deltaTime;

            _currentTime = Mathf.RoundToInt(_currentLevelTime);

            _timerText.text = $"{_currentTime} SEC";
        }
        else
        {
            _timerText.text = $"0 SEC";
            _levelResulter.LevelRestart();
            _currentLevelTime = _allLevelTime;
        }
        
    }
}
