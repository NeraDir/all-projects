using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelManager : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private Image _closePanel;

    [SerializeField]
    private int _levelIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_closePanel.gameObject.activeInHierarchy)
            return;
        GameController.punkCrystallCurrentLevelIndex = _levelIndex;
        SceneManager.LoadScene("main");
    }

    private void Start()
    {
        PlayerPrefs.SetInt("PunkCrystallsSOgiidfugsduigdfsiogfds" + 1 + "saves", 1);
        if (PlayerPrefs.HasKey("PunkCrystallsSOgiidfugsduigdfsiogfds"+_levelIndex+"saves"))
        {
            _closePanel.gameObject.SetActive(false);
        }
        else
        {
            _closePanel.gameObject.SetActive(true);
        }
    }
}
