using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoaingComponent : MonoBehaviour
{
    [SerializeField]
    private string _sceneNameOfIndex;

    [SerializeField]
    private string _sceneAdditionalNameOfIndex;

    [SerializeField]
    private float _sceneLoadingTime;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private float _imageLoadingTime;

    [SerializeField]
    private float _speedOFLoading;

    private static string _gamingLoadingName;

    private static string _loadingerName;

    public bool isTemper;

    private void Start() 
    {
        _gamingLoadingName = _sceneNameOfIndex;
        _loadingerName = _sceneAdditionalNameOfIndex;

        if (isTemper)
            StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        while (_imageLoadingTime < _sceneLoadingTime) 
        {
            _imageLoadingTime = Mathf.MoveTowards(_imageLoadingTime, _sceneLoadingTime, _speedOFLoading * Time.deltaTime);
            if (_image != null)
            {
                _image.fillAmount = _imageLoadingTime / _sceneLoadingTime;
            }
            yield return null;
        }
        SceneManager.LoadScene(_sceneNameOfIndex);
    }

    public static void LoadGameObject() 
    {
        SceneManager.LoadScene(_gamingLoadingName);
    }

    public static void LoadAdditionalScene()
    {
        SceneManager.LoadScene(_loadingerName);
    }
}
