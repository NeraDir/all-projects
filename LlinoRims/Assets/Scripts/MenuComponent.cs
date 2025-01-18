using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuPage;

    [SerializeField]
    private GameObject _hwtPage;

    [SerializeField]
    private LevelContainer _levelContainer;

    [SerializeField]
    private Transform _levelSpawnPosition;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("LlinoRealmsHwtSaveKey"))
        {
            _hwtPage.SetActive(true);
            _menuPage.SetActive(false);
            PlayerPrefs.SetInt("LlinoRealmsHwtSaveKey",1);
        }
        for (int i = 0; i < 24; i++)
        {
            LevelContainer tempContainer = Instantiate(_levelContainer, _levelSpawnPosition);
            tempContainer.levelIndex = i;
        }
    }
}
