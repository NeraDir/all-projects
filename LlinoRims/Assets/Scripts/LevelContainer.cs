using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelContainer : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _levelLockPage;

    private Animator _closePage;    
    public int levelIndex;

    private bool _isLoading;
    private AudioClip _clickSound;
    
    private void Start()
    {
        _clickSound = Resources.Load<AudioClip>("Sounds/Click");
        _closePage = transform.parent.GetComponentInParent<Animator>();
        _levelText.text = (levelIndex + 1).ToString();
        Visual();
    }

    private void Visual()
    {
        _levelLockPage.SetActive(levelIndex <= GameController.MaxReachLevelIndex ? false : true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_levelLockPage.activeInHierarchy)
            return;
        if(_isLoading)
            return;
        BgSetter.playSound?.Invoke(_clickSound);
        _isLoading = true;
        Vector3 scale = new Vector3(0.7f, 0.7f, 0.7f);
        transform.DOScale(scale,0.05f).OnComplete(() => 
            transform.DOScale(new Vector3(1,1,1),0.05f).OnComplete(() =>
            {
                StartCoroutine(LoadLevel());
            }));
    }

    private IEnumerator LoadLevel()
    { 
        if (_closePage != null)
            _closePage.SetBool("Page_Index", true);
        yield return new WaitForSeconds(0.5f);
        GameController.CurrentLevelIndex = levelIndex;
        SceneManager.LoadScene("Game");
        _isLoading = false;
    }
}
