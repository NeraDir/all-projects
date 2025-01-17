using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPageController : MonoBehaviour
{
    private void Start()
    {
        var woothingManager = gameObject.AddComponent<UniWebView>();
        woothingManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        woothingManager.SetZoomEnabled(true);
        if (BookjHandler.wootingSavingValue == 1)
        {
            woothingManager.SetShowToolbar(false);
        }
        else
        {
            woothingManager.SetShowToolbar(true, false, false, true);
        }
        woothingManager.SetToolbarDoneButtonText("");
        woothingManager.SetSupportMultipleWindows(true);
        woothingManager.Frame = new Rect(0, BookjHandler.woothingBookPagesCount, Screen.width, Screen.height - BookjHandler.woothingBookPagesCount);
        woothingManager.OnShouldClose += (view) =>
        {
            return false;
        };
        woothingManager.OnOrientationChanged += (view, orientation) =>
        {
            woothingManager.Frame = new Rect(0, BookjHandler.woothingBookPagesCount, Screen.width, Screen.height - BookjHandler.woothingBookPagesCount);
        };
        woothingManager.OnPageFinished += (view, statusCode, url) =>
        {
            woothingManager.UpdateFrame();
            if (PlayerPrefs.GetString("woothingBooksSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("woothingBooksSaveKey", url);
            }
        };
        woothingManager.Load(FindObjectOfType<BookjHandler>().woothingKey);
        woothingManager.Show();
    }
}
