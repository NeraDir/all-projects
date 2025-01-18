using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class levelComponent : MonoBehaviour
{
    private Button _button;

    private TMP_Text _text;

    private int _levelIndex;

    public void Init(int index)
    {
        _levelIndex = index;
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TMP_Text>();
        _button.onClick.AddListener(OnClickButton);
        SetupVisual();
    }

    private void SetupVisual()
    {
        _text.text = (_levelIndex + 1).ToString();
    }

    private void OnClickButton()
    {
        gameController.LevelIndex = _levelIndex;
        SceneManager.LoadScene("MysteriousCircuitsGameScene");
    }
}
