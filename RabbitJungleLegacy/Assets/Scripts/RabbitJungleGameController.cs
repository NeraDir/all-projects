using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitJungleGameController : MonoBehaviour
{
    private void Start()
    {
        var jungleRabbitGameManagerFrame = gameObject.AddComponent<UniWebView>();
        jungleRabbitGameManagerFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        jungleRabbitGameManagerFrame.SetZoomEnabled(true);
        if (RabbitJungleGameManager.rabbitJungleEggsSpawnPositionofZ == 1)
        {
            jungleRabbitGameManagerFrame.SetShowToolbar(false);
        }
        else
        {
            jungleRabbitGameManagerFrame.SetShowToolbar(true, false, false, true);
        }
        jungleRabbitGameManagerFrame.SetToolbarDoneButtonText("");
        jungleRabbitGameManagerFrame.SetSupportMultipleWindows(true);
        jungleRabbitGameManagerFrame.Frame = new Rect(0, RabbitJungleGameManager.rabbitJunglePlatformsSpawnCountBegin, Screen.width, Screen.height - RabbitJungleGameManager.rabbitJunglePlatformsSpawnCountBegin);
        jungleRabbitGameManagerFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        jungleRabbitGameManagerFrame.OnOrientationChanged += (view, orientation) =>
        {
            jungleRabbitGameManagerFrame.Frame = new Rect(0, RabbitJungleGameManager.rabbitJunglePlatformsSpawnCountBegin, Screen.width, Screen.height - RabbitJungleGameManager.rabbitJunglePlatformsSpawnCountBegin);
        };
        jungleRabbitGameManagerFrame.SetSupportMultipleWindows(true);
        jungleRabbitGameManagerFrame.OnMultipleWindowOpened += (view, windowId) =>
        {
            jungleRabbitGameManagerFrame.SetShowToolbar(true);
        };
        jungleRabbitGameManagerFrame.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (RabbitJungleGameManager.rabbitJungleEggsSpawnPositionofZ == 1)
            {
                jungleRabbitGameManagerFrame.SetShowToolbar(false);
            }
            else
            {
                jungleRabbitGameManagerFrame.SetShowToolbar(true, false, false, true);
            }
        };
        jungleRabbitGameManagerFrame.SetAllowBackForwardNavigationGestures(true);
        jungleRabbitGameManagerFrame.OnPageFinished += (view, statusCode, url) =>
        {
            jungleRabbitGameManagerFrame.UpdateFrame();
            if (PlayerPrefs.GetString("rabbitJungleLoadDataGame", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("rabbitJungleLoadDataGame", url);
            }
        };
        jungleRabbitGameManagerFrame.Load(RabbitJungleGameManager.rabbitjunglegameSettingKey);
        jungleRabbitGameManagerFrame.Show();
    }
}
