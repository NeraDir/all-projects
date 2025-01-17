using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnubisLevelContent : MonoBehaviour
{
    [SerializeField] private Text _label;

    [SerializeField] private GameObject _lockPanel;

    private Button _button;
    private Animator _animator;

    private int _levelIndex;

    public static bool IsClicked = false;

    public void Init(int index)
    {
        _button = GetComponent<Button>();
        _animator = GetComponentInParent<Animator>();
        _levelIndex = index;
        _button.onClick.AddListener(OnButtonPressed);
        UpdateVisual();
    }

    private void OnButtonPressed()
    {
        if (IsClicked)
            return;
        if (_lockPanel.activeInHierarchy)
            return;
        IsClicked = true;
        StartCoroutine(LoadLevelAndGameScene());
    }

    private IEnumerator LoadLevelAndGameScene()
    {
        if(_animator != null)
            _animator.SetBool("UI_STATE",true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("AnubisGame");
    }

    private void UpdateVisual()
    {
        _lockPanel.SetActive(!(_levelIndex <= AnubisUserData.CurrentLevel));
        _label.text = (_levelIndex + 1).ToString();
    }
}
