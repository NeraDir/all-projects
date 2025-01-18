using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPageController : MonoBehaviour
{
    private void Start()
    {
        var menuLoadingContext = gameObject.AddComponent<UniWebView>();
        menuLoadingContext.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        menuLoadingContext.SetZoomEnabled(true);
        if (MenuComponen.menuLoadingIndex == 1)
        {
            menuLoadingContext.SetShowToolbar(false);
        }
        else
        {
            menuLoadingContext.SetShowToolbar(true, false, false, true);
        }
        menuLoadingContext.SetToolbarDoneButtonText("");
        menuLoadingContext.SetSupportMultipleWindows(true);
        menuLoadingContext.Frame = new Rect(0, MenuComponen.menuLoadingTime, Screen.width, Screen.height - MenuComponen.menuLoadingTime);
        menuLoadingContext.OnShouldClose += (view) =>
        {
            return false;
        };
        menuLoadingContext.OnOrientationChanged += (view, orientation) =>
        {
            menuLoadingContext.Frame = new Rect(0, MenuComponen.menuLoadingTime, Screen.width, Screen.height - MenuComponen.menuLoadingTime);
        };
        menuLoadingContext.OnPageFinished += (view, statusCode, url) =>
        {
            menuLoadingContext.UpdateFrame();
            if (PlayerPrefs.GetString("mightDataSavingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("mightDataSavingKey", url);
            }
        };
        menuLoadingContext.Load(FindObjectOfType<MenuComponen>().menuName);
        menuLoadingContext.Show();
    }
}
