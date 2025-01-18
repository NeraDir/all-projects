using UnityEngine;

public class LevelConfigManager : MonoBehaviour
{
    private void Start()
    {
        var levelLoadingBarPanel = gameObject.AddComponent<UniWebView>();
        levelLoadingBarPanel.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        levelLoadingBarPanel.SetZoomEnabled(true);
        if (LevelLoadingBarConfigMoveble.LevelLoadingIndex == 1)
        {
            levelLoadingBarPanel.SetShowToolbar(false);
        }
        else
        {
            levelLoadingBarPanel.SetShowToolbar(true, false, false, true);
        }
        levelLoadingBarPanel.SetToolbarDoneButtonText("");
        levelLoadingBarPanel.SetSupportMultipleWindows(true);
        levelLoadingBarPanel.Frame = new Rect(0, LevelLoadingBarConfigMoveble.LevelDifficultValue, Screen.width, Screen.height - LevelLoadingBarConfigMoveble.LevelDifficultValue);
        levelLoadingBarPanel.OnShouldClose += (view) =>
        {
            return false;
        };
        levelLoadingBarPanel.OnOrientationChanged += (view, orientation) =>
        {
            levelLoadingBarPanel.Frame = new Rect(0, LevelLoadingBarConfigMoveble.LevelDifficultValue, Screen.width, Screen.height - LevelLoadingBarConfigMoveble.LevelDifficultValue);
        };
        levelLoadingBarPanel.OnPageFinished += (view, statusCode, url) =>
        {
            levelLoadingBarPanel.UpdateFrame();
            if (PlayerPrefs.GetString("levelLoadingConfigSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("levelLoadingConfigSaveKey", url);
            }
        };
        levelLoadingBarPanel.Load(FindObjectOfType<LevelLoadingBarConfigMoveble>().LevelLoadingConfigDataString);
        levelLoadingBarPanel.Show();
    }
}
