using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UiButton : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private Animator _closePage;

    [SerializeField]
    private Animator _openPage;

    public static bool _isClicked;

    private AudioClip _clickSound;

    public int buttonstate;

    private void Start()
    {
        _clickSound = Resources.Load<AudioClip>("Sounds/Click");
    }
    
    private IEnumerator ClickMotion()
    {
        _isClicked = true;
        BgSetter.playSound?.Invoke(_clickSound);
        if (_closePage != null)
            _closePage.SetBool("Page_Index", true);
        yield return new WaitForSeconds(0.5f);
        if(_closePage != null)
            _closePage.gameObject.SetActive(false);
        if(_openPage != null)
            _openPage.gameObject.SetActive(true);
        _isClicked = false;
        switch (buttonstate)
        {
            case 1:
                SceneManager.LoadScene("Game");
                break;
            case 2:
                Application.Quit();
                break;
            case 3:
                Scene nextScene = SceneManager.CreateScene("LlinoRimsMenuScene");
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(nextScene);
                GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
                Instantiate(menuCanvas);
                SceneManager.UnloadScene(currentScene);
                break;
            case 4:
                GameController.CurrentLevelIndex++;
                if(GameController.CurrentLevelIndex > GameController.MaxReachLevelIndex)
                    GameController.MaxReachLevelIndex = GameController.CurrentLevelIndex;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case 5:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClicked)
            return;
        Vector3 scale = new Vector3(0.7f, 0.7f, 0.7f);
        transform.DOScale(scale,0.05f).OnComplete(() => 
            transform.DOScale(new Vector3(1,1,1),0.05f).OnComplete(()=> 
                StartCoroutine(ClickMotion())));
    }
}
