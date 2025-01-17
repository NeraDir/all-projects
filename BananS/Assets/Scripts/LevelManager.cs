using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text swetiecountTMPUI;
    [SerializeField]
    private TMP_Text levelNumberTMPUI;

    [SerializeField]
    private int sceneIndex;
    [SerializeField]
    private int MaxStartCount;

    public static int _maxStarsCount;

    [SerializeField]
    private string nextLevelSceneKey;

    [SerializeField]
    private GameObject gameOverPanel;

    private void OnEnable()
    {
        UI_LevelCompleteLayer.TapToNextLevelBuutonEvent += SetNextLevel;
        UI_LevelCompleteLayer.TapToRestartLevelBuutonEvent += RestartLevel;
        UI_LevelCompleteLayer.TapToMenuBuutonEvent += LoadMenu;

        GameOverTrigger.HeadSweetieTriggerEvent += ShowGameOver;
        HeadSweetie.ObstacleTriggerEvent += ShowGameOver;


        _maxStarsCount = MaxStartCount;
        ParametersPerformer.actualLevel = sceneIndex;
        levelNumberTMPUI.text = "LEVEL " + ParametersPerformer.actualLevel;
    }
    private void OnDisable()
    {
        UI_LevelCompleteLayer.TapToNextLevelBuutonEvent -= SetNextLevel;
        UI_LevelCompleteLayer.TapToRestartLevelBuutonEvent -= RestartLevel;
        UI_LevelCompleteLayer.TapToMenuBuutonEvent -= LoadMenu;

        GameOverTrigger.HeadSweetieTriggerEvent -= ShowGameOver;
        HeadSweetie.ObstacleTriggerEvent -= ShowGameOver;
    }

    private void Update()
    {
        swetiecountTMPUI.text = (ParametersPerformer.sweetieCount == 0 ? "X0" : ParametersPerformer.sweetieCount.ToString("X#"));
    }




    private void SetNextLevel()
    {
        if (ParametersPerformer.actualLevel > ParametersPerformer.recordLevel)
        {
            ParametersPerformer.recordLevel = ParametersPerformer.actualLevel;
        }
        SceneManager.LoadScene(nextLevelSceneKey);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void LoadMenu()
    {
        SceneManager.LoadScene("JellyPeaks_MENU_SCENE");
    }


    private void ShowGameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
    }

    public static void LoadDefaultLevels()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("JellyPeaks_LOAD_SCENE");
    }
    public static void LoadLightLevel(string lightLevelSceneKey)
    {
        ParametersPerformer.recordLevelSceneKey = lightLevelSceneKey;
        SceneManager.LoadScene("JellyPeaks_MENU_GAME_LEVEL_lith");
    }
}
