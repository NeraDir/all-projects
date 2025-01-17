using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuComponent : MonoBehaviour
{
    [SerializeField]private Button _playButton;
    [SerializeField]private Button _exitButton;
    [SerializeField]private GameObject _aboutInfoScreen;
    [SerializeField]private TMP_Text _maxReachWaveDisplay;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CaramelTreath_aboutInfoShowedKey"))
        {
            _aboutInfoScreen.SetActive(true);
            PlayerPrefs.SetInt("CaramelTreath_aboutInfoShowedKey", 1);
        }
        _maxReachWaveDisplay.text = GameComponent.CaramelTreatMaxWave.ToString();
        _playButton.onClick.AddListener(OnClickPlay);
        _exitButton.onClick.AddListener(OnClickExit);
    }

    private void OnClickPlay()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Scene gameScene = SceneManager.CreateScene("GameScene");
        SceneManager.SetActiveScene(gameScene);
        Instantiate(Resources.Load("Prefabs/Game"));
        SceneManager.UnloadScene(currentScene);
    }

    private void OnClickExit()
    {
        Application.Quit();
    }
}
