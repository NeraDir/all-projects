using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ButtonType
{
    Mover,
    Exit,
    Play,
    Menu,
    Restart,
}

[RequireComponent(typeof(Button))]
public class ChaseCustomButton : MonoBehaviour
{
    [SerializeField] private GameObject _chaseOpenPage;
    [SerializeField] private GameObject _chaseClosePage;
    [SerializeField] private ButtonType _buttonType;
    
    public static bool _isClicked = false;
    
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            if(_isClicked)
                return;
            Action callback = null;
            ChaseSettingsManager.playSound?.Invoke();
            switch (_buttonType)
            {
                case ButtonType.Mover:
                    callback = OnMover;
                    break;
                case ButtonType.Exit:
                    callback = OnExit;
                    break;
                case ButtonType.Play:
                    callback = OnPlay;
                    break;
                case ButtonType.Menu:
                    callback = OnMenu;
                    break;
                case ButtonType.Restart:
                    callback = OnRestart;
                    break;
            }
            StartCoroutine(OnButtonPressed(callback));
        });
    }

    private IEnumerator OnButtonPressed(Action callback = null)
    {
        _isClicked = true;
        if(_chaseClosePage != null){
            Animator animator = _chaseClosePage.GetComponent<Animator>();
            if(animator != null)
                animator.SetBool("ChasePageState", true);
        }
        yield return new WaitForSeconds(0.5f);
        if(_chaseClosePage != null)
            _chaseClosePage.SetActive(false);
        if(_chaseOpenPage != null)
            _chaseOpenPage.SetActive(true);
        callback?.Invoke();
        _isClicked = false;
    }

    private void OnMover()
    {
        
    }

    private void OnExit()
    {
        Application.Quit();
    }

    private void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Scene nextScene = SceneManager.CreateScene("CheaseMenuScene");
        SceneManager.SetActiveScene(nextScene);
        SceneManager.UnloadScene(currentScene);
        GameObject menuObject = Resources.Load<GameObject>("Prefabs/ChaseMenuPrefab");
        Instantiate(menuObject);
    }

    private void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }
}
