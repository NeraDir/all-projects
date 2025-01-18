using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelElementComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _lockPanel;
    
    private TMP_Text _levelText;

    private int _levelIndex;
    
    public static bool isClicked;
    
    public void Init(int index)
    {
        _levelText = GetComponentInChildren<TMP_Text>();
        _levelIndex = index;
        if (_levelIndex <= GameManager.TigerMaxReachedLevel)
            _lockPanel.SetActive(false);
        _levelText.text = (_levelIndex + 1).ToString();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_lockPanel.activeInHierarchy)
            return;
        if(isClicked)
            return;
        isClicked = true;
        transform.DOScale(Vector3.one / 1.5f, 0.25f).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, 0.25f).OnComplete(() =>
            {
                isClicked = false;
                GameManager.TigerCurrentLevel = _levelIndex;
                SceneManager.LoadScene("ForTenGameScene");
            });
        });
    }
}
