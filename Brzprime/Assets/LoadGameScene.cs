using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
    [SerializeField]
    private Text _loadingValueShow;

    [SerializeField]
    private Image _loadingProgressBar;

    private float _currentLoadingValue;

    [SerializeField]
    private float _loadingMaxProgress;

    [SerializeField]
    private float _loadingPieceOfLoad;

    [SerializeField]
    private string _loadingSceneName;

    private IEnumerator Start()
    {
        while (_currentLoadingValue < _loadingMaxProgress) 
        {
            _currentLoadingValue = Mathf.MoveTowards(_currentLoadingValue,_loadingMaxProgress + 10,_loadingPieceOfLoad * Time.deltaTime);
            yield return null;
        }
        SceneManager.LoadScene(_loadingSceneName);
        StopAllCoroutines();
    }

    private void LateUpdate()
    {
        if (_loadingProgressBar != null) 
        {
            _loadingProgressBar.fillAmount = Mathf.Lerp(_loadingProgressBar.fillAmount, _currentLoadingValue / _loadingMaxProgress, 8 * Time.deltaTime);
            _loadingValueShow.text = _currentLoadingValue.ToString("0") + "%";
        }
    }
}
