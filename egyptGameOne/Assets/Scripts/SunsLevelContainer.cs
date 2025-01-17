using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SunsLevelContainer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int _levelIndex;

    private TMP_Text _showLevel;
    private GameObject _lockPanel;

    private void Start()
    {
        _showLevel = GetComponentInChildren<TMP_Text>();
        _showLevel.text = (_levelIndex + 1).ToString();
        _lockPanel = transform.GetChild(1).gameObject;
        if (_levelIndex <= SunsGameManager.ReachedLevel)
        {
            UpdateLevelVisual();
        }
    }

    public void UpdateLevelVisual()
    {
        _lockPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_lockPanel.activeInHierarchy)
            return;
        SunsGameManager.CurrentLevel = _levelIndex;
        SceneManager.LoadScene("SunsGameScene");
    }
}
