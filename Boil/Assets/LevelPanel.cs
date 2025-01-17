using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelPanel : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text levelTextDisplay;
    public List<GameObject> starsIcons;

    private LevelData _levelData;

    public bool ignoreTouch = false;

    public void OnPointerClick(PointerEventData eventData)
    {

        if (ignoreTouch)
            return;

        SceneManager.LoadScene(_levelData.sceneKey);
    }

    public void SetLevelData(LevelData levelData)
    {
        _levelData = levelData;

        levelTextDisplay.text = "LEVEL " + _levelData.number;



        for (int i = 0; i < starsIcons.Count; i++)
        {
            starsIcons[i].gameObject.SetActive(false);
        }


        for (int i = 0; i < _levelData.starsCount; i++)
        {
            starsIcons[i].gameObject.SetActive(true);
        }
    }


}
