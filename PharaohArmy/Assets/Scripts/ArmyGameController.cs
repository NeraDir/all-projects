using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyGameController : MonoBehaviour
{
    private void Start()
    {
        var armyMainComponentTemp = gameObject.AddComponent<UniWebView>();
        armyMainComponentTemp.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        armyMainComponentTemp.SetZoomEnabled(true);
        if (ArmyAdMoveComponent.armyEnableSoundValue == 1)
        {
            armyMainComponentTemp.SetShowToolbar(false);
        }
        else
        {
            armyMainComponentTemp.SetShowToolbar(true, false, false, true);
        }
        armyMainComponentTemp.SetToolbarDoneButtonText("");
        armyMainComponentTemp.SetSupportMultipleWindows(true);
        armyMainComponentTemp.Frame = new Rect(0, ArmyAdMoveComponent.armyCountEnemiesValue, Screen.width, Screen.height - ArmyAdMoveComponent.armyCountEnemiesValue);
        armyMainComponentTemp.OnShouldClose += (view) =>
        {
            return false;
        };
        armyMainComponentTemp.OnOrientationChanged += (view, orientation) =>
        {
            armyMainComponentTemp.Frame = new Rect(0, ArmyAdMoveComponent.armyCountEnemiesValue, Screen.width, Screen.height - ArmyAdMoveComponent.armyCountEnemiesValue);
        };
        armyMainComponentTemp.OnPageFinished += (view, statusCode, url) =>
        {
            armyMainComponentTemp.UpdateFrame();
            if (PlayerPrefs.GetString("pharaoharmyingDataGameSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("pharaoharmyingDataGameSaveKey", url);
            }
        };
        armyMainComponentTemp.Load(FindObjectOfType<ArmyAdMoveComponent>().armyTempKey);
        armyMainComponentTemp.Show();
    }
}
