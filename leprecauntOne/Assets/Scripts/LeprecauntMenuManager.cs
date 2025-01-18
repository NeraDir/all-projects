using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeprecauntMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _lepreHowToPlay;

    [SerializeField]
    private LeprecountLevelContainer _levelPrefab;

    [SerializeField]
    private Transform _spawnPos;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("LeprecountLegacyHowToPlaySeesKey"))
        {
            _lepreHowToPlay.SetActive(true);
            PlayerPrefs.SetInt("LeprecountLegacyHowToPlaySeesKey", 1);
        }
        for (int i = 0; i < LevelDatasLoader.LevelDatas.Count; i++)
        {
            LeprecountLevelContainer tempContainer = Instantiate(_levelPrefab, _spawnPos);
            tempContainer._levelIndex = i;
            if (i <= LeprecauntGamemanager.MaxReachLevel)
            {
                tempContainer.UpdateVisual();
            }
        }
    }

    public void OnPressQuitGame()
    {
        Application.Quit();
    }
}
