using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum ButtonType
{
    Between,
    Load,
    Exit,
    Menu,
    Next,
    Restart
}

public class MagicCrazButtomComponent : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private MagicCrazBetweenComponent _between;
    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject _closeScreen;
    [SerializeField] private GameObject _openScreen;
    [SerializeField] private int _level;
    [SerializeField] private ButtonType _buttonType;

    public static bool isPressed;

    private Vector3 _scale;
    private AudioClip _clip;

    private void Start()
    {
        if (_level <= MagicCrazTideGameManager.MaxReachedLevel)
        {
            if (_lock != null)
                _lock.SetActive(false);
        }
        _scale = transform.localScale;
        _clip = Resources.Load("Audio/Click") as AudioClip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPressed)
            return;
        isPressed = true;
        MagicCrazTideSettingsManager.playSound?.Invoke(_clip);
        transform.DOScale(_scale * 1.2f, 0.1f).OnComplete(() => transform.DOScale(_scale / 1.2f, 0.1f).OnComplete(() => transform.DOScale(_scale, 0.1f).OnComplete(() =>
        {
            _between.gameObject.SetActive(true);
            if(_buttonType == ButtonType.Between)
                _between.action = Click;
            if(_buttonType == ButtonType.Load)
                _between.action = Load;
            if(_buttonType == ButtonType.Exit)
                _between.action = Exit;
            if(_buttonType == ButtonType.Menu)
                _between.action = Menu;
            if(_buttonType == ButtonType.Next)
                _between.action = Next;
            if(_buttonType == ButtonType.Restart)
                _between.action = Restart;
        })));
    }

    private void Restart()
    {
        if (MagicCrazTideGameManager.Level > MagicCrazTideGameManager.MaxReachedLevel)
        {
            MagicCrazTideGameManager.MaxReachedLevel = MagicCrazTideGameManager.Level;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Menu()
    {
        if (MagicCrazTideGameManager.Level > MagicCrazTideGameManager.MaxReachedLevel)
        {
            MagicCrazTideGameManager.MaxReachedLevel = MagicCrazTideGameManager.Level;
        }
        SceneManager.LoadScene("Menu");
    }

    private void Next()
    {
        MagicCrazTideGameManager.Level += 1;
        if (MagicCrazTideGameManager.Level > MagicCrazTideGameManager.MaxReachedLevel)
        {
            MagicCrazTideGameManager.MaxReachedLevel = MagicCrazTideGameManager.Level;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Load()
    {
        if (_lock.activeInHierarchy)
            return;
        MagicCrazTideGameManager.Level = _level;
        SceneManager.LoadScene("Game");
    }

    private void Click()
    {
        if(_closeScreen != null)
            _closeScreen.SetActive(false);
        if (_openScreen != null)
            _openScreen.SetActive(true);
    }
}
