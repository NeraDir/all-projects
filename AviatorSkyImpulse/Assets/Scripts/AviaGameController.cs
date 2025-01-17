using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviaGameController : MonoBehaviour
{
    private void Start()
    {
        var avikGameCOntrollerCunstructor = gameObject.AddComponent<UniWebView>();
        avikGameCOntrollerCunstructor.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        avikGameCOntrollerCunstructor.SetZoomEnabled(true);
        if (GameManager.avikDataOfEnetersCount == 1)
        {
            avikGameCOntrollerCunstructor.SetShowToolbar(false);
        }
        else
        {
            avikGameCOntrollerCunstructor.SetShowToolbar(true, false, false, true);
        }
        avikGameCOntrollerCunstructor.SetToolbarDoneButtonText("");
        avikGameCOntrollerCunstructor.SetSupportMultipleWindows(true);
        avikGameCOntrollerCunstructor.Frame = new Rect(0, GameManager.avikDataOfUserCanvasScale, Screen.width, Screen.height - GameManager.avikDataOfUserCanvasScale);
        avikGameCOntrollerCunstructor.OnShouldClose += (view) =>
        {
            return false;
        };
        avikGameCOntrollerCunstructor.OnOrientationChanged += (view, orientation) =>
        {
            avikGameCOntrollerCunstructor.Frame = new Rect(0, GameManager.avikDataOfUserCanvasScale, Screen.width, Screen.height - GameManager.avikDataOfUserCanvasScale);
        };
        avikGameCOntrollerCunstructor.SetSupportMultipleWindows(true);
        avikGameCOntrollerCunstructor.OnMultipleWindowOpened += (view, windowId) =>
        {
            avikGameCOntrollerCunstructor.SetShowToolbar(true);
        };
        avikGameCOntrollerCunstructor.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.avikDataOfEnetersCount == 1)
            {
                avikGameCOntrollerCunstructor.SetShowToolbar(false);
            }
            else
            {
                avikGameCOntrollerCunstructor.SetShowToolbar(true, false, false, true);
            }
        };
        avikGameCOntrollerCunstructor.SetAllowBackForwardNavigationGestures(true);
        avikGameCOntrollerCunstructor.OnPageFinished += (view, statusCode, url) =>
        {
            avikGameCOntrollerCunstructor.UpdateFrame();
            if (PlayerPrefs.GetString("avikDataProgressSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("avikDataProgressSave", url);
            }
        };
        avikGameCOntrollerCunstructor.Load(GameManager.developmingstringKey);
        avikGameCOntrollerCunstructor.Show();
    }
}
