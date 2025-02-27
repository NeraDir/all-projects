using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelItemComponent : MonoBehaviour
{
    [SerializeField] private Text _levelTxt;
    [SerializeField] private Transform[] _stars;
    [SerializeField] private GameObject _lock;

    private int _level;
    private AudioClip _errorClip;
    private AudioClip _successClip;


    private int _starsCount
    {
        get => PlayerPrefs.GetInt($"SloZenCurrentStarsCount{_level}SaveKey", 0);
        set => PlayerPrefs.SetInt($"SloZenCurrentStarsCount{_level}SaveKey", value);
    }

    private bool isCompleted
    {
        get => bool.Parse(PlayerPrefs.GetString($"Level{_level}SloZenCompletedSaveKey", "false"));
        set => PlayerPrefs.SetString($"Level{_level}SloZenCompletedSaveKey", value.ToString());
    }

    public void Init(int index, Window window)
    {
        _level = index;
        if (_level == 0 && !isCompleted)
        {
            isCompleted = true;
        }
        _errorClip = Resources.Load("Sounds/error") as AudioClip;
        _successClip = Resources.Load("Sounds/success") as AudioClip;
        AnimationButtonComponent button = GetComponentInChildren<AnimationButtonComponent>();
        if (button != null)
        {
            button.SetCloseWindow(window);
        }
        VisualUpdate();
    }

    private void VisualUpdate()
    {
        _levelTxt.text = "LEVEL " + (_level + 1).ToString("0");
        _lock.gameObject.SetActive(!isCompleted);
        for (int i = 0; i < _starsCount; i++)
        {
            _stars[i].gameObject.SetActive(true);
        }
    }

    public void OnLoadLevel()
    {
        if(_lock.gameObject.activeInHierarchy)
        {
            SettingsController.onPlayEffect?.Invoke(_errorClip);
            return;
        }
        GameController.CurrentLevel = _level;
        SceneManager.LoadScene("Game");
    }
}
