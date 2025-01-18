using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeprecountLevelContainer : MonoBehaviour, IPointerClickHandler
{
    public int _levelIndex;

    [SerializeField]
    private TMP_Text _displayLevel;

    [SerializeField]
    private GameObject _blackPanel;

    [SerializeField]
    private Image[] _starsImages;

    private void Start()
    {
        _displayLevel.text = (_levelIndex + 1).ToString();
    }

    public void UpdateVisual()
    {
        _blackPanel.SetActive(false);
        int starsCount = PlayerPrefs.GetInt($"{_levelIndex}LevelStarsCountLeprecaount");
        for (int i = 0; i < starsCount; i++)
        {
            _starsImages[i].color = Color.white;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_blackPanel.activeInHierarchy)
            return;
        LeprecauntGamemanager._currentLevel = _levelIndex;
        SceneManager.LoadScene("LeprecauntGame");
    }
}
