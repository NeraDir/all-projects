using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuComponent : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;

    [SerializeField] private GameObject _aboutPage;
    [SerializeField] private GameObject _menuPage;

    private void Start()
    {
        CustomButton.isPressed = false;
        if (!PlayerPrefs.HasKey("FirstEntryDataSaveKey"))
        {
            _aboutPage.SetActive(true);
            _menuPage.SetActive(false);
            PlayerPrefs.SetString("FirstEntryDataSaveKey", "YES");
        }
        for (int i = 0; i < _panels.Length; i++)
        {
            if(i <= GameComponent.MaxLevel-1)
                _panels[i].SetActive(false);
        }
    }

    public void Play(int index)
    {
        if (_panels[index-1].activeInHierarchy)
            return;
        GameComponent.Level = index;
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
