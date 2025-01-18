using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    private GameObject _closePanel;

    [SerializeField]
    private int index;

    private TMP_Text _levelIndexTxt;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_closePanel.activeInHierarchy)
            return;
        GameController.levelIndex = index;
        SceneManager.LoadScene("Game");
    }

    private void Start()
    {
        _levelIndexTxt = GetComponentInChildren<TMP_Text>();
        _levelIndexTxt.text = (index + 1).ToString();
        if (index <= GameController.MaxReachLevel)
        {
            _closePanel.SetActive(false);
        }
    }
}
