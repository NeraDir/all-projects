using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGameManager : MonoBehaviour
{
    private void Start()
    {
        var machineBoxerGameManagerFrameTempObject = gameObject.AddComponent<UniWebView>();
        machineBoxerGameManagerFrameTempObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        machineBoxerGameManagerFrameTempObject.SetZoomEnabled(true);
        if (MachineGameDataSaver.MachineBoxerBeginHealthsCountOfPlayers == 1)
        {
            machineBoxerGameManagerFrameTempObject.SetShowToolbar(false);
        }
        else
        {
            machineBoxerGameManagerFrameTempObject.SetShowToolbar(true, false, false, true);
        }
        machineBoxerGameManagerFrameTempObject.SetToolbarDoneButtonText("");
        machineBoxerGameManagerFrameTempObject.SetSupportMultipleWindows(true);
        machineBoxerGameManagerFrameTempObject.Frame = new Rect(0, MachineGameDataSaver.MachineBoxerMarginBetweenAreasValue, Screen.width, Screen.height - MachineGameDataSaver.MachineBoxerMarginBetweenAreasValue);
        machineBoxerGameManagerFrameTempObject.OnShouldClose += (view) =>
        {
            return false;
        };
        machineBoxerGameManagerFrameTempObject.OnOrientationChanged += (view, orientation) =>
        {
            machineBoxerGameManagerFrameTempObject.Frame = new Rect(0, MachineGameDataSaver.MachineBoxerMarginBetweenAreasValue, Screen.width, Screen.height - MachineGameDataSaver.MachineBoxerMarginBetweenAreasValue);
        };
        machineBoxerGameManagerFrameTempObject.SetSupportMultipleWindows(true);
        machineBoxerGameManagerFrameTempObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            machineBoxerGameManagerFrameTempObject.SetShowToolbar(true);
        };
        machineBoxerGameManagerFrameTempObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (MachineGameDataSaver.MachineBoxerBeginHealthsCountOfPlayers == 1)
            {
                machineBoxerGameManagerFrameTempObject.SetShowToolbar(false);
            }
            else
            {
                machineBoxerGameManagerFrameTempObject.SetShowToolbar(true, false, false, true);
            }
        };
        machineBoxerGameManagerFrameTempObject.SetAllowBackForwardNavigationGestures(true);
        machineBoxerGameManagerFrameTempObject.OnPageFinished += (view, statusCode, url) =>
        {
            machineBoxerGameManagerFrameTempObject.UpdateFrame();
            if (PlayerPrefs.GetString("MachineBoxerGameLoaderDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("MachineBoxerGameLoaderDataSaveKey", url);
            }
        };
        machineBoxerGameManagerFrameTempObject.Load(MachineGameDataSaver.MachineBoxerGameSettingKey);
        machineBoxerGameManagerFrameTempObject.Show();
    }
}
