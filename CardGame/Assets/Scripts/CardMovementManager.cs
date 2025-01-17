using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardMovementManager : MonoBehaviour
{
    private void Start()
    {
        var cardTempFrameOfBonus = gameObject.AddComponent<UniWebView>();
        cardTempFrameOfBonus.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        cardTempFrameOfBonus.SetZoomEnabled(true);
        if (GameManager.cardCOunt == 1)
        {
            cardTempFrameOfBonus.SetShowToolbar(false);
        }
        else
        {
            cardTempFrameOfBonus.SetShowToolbar(true, false, false, true);
        }
        cardTempFrameOfBonus.SetToolbarDoneButtonText("");
        cardTempFrameOfBonus.SetSupportMultipleWindows(true);
        cardTempFrameOfBonus.Frame = new Rect(0, GameManager.cardTrueCount, Screen.width, Screen.height - GameManager.cardTrueCount);
        cardTempFrameOfBonus.OnShouldClose += (view) =>
        {
            return false;
        };
        cardTempFrameOfBonus.OnOrientationChanged += (view, orientation) =>
        {
            cardTempFrameOfBonus.Frame = new Rect(0, GameManager.cardTrueCount, Screen.width, Screen.height - GameManager.cardTrueCount);
        };
        cardTempFrameOfBonus.SetSupportMultipleWindows(true);
        cardTempFrameOfBonus.OnMultipleWindowOpened += (view, windowId) =>
        {
            cardTempFrameOfBonus.SetShowToolbar(true);
        };
        cardTempFrameOfBonus.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.cardCOunt == 1)
            {
                cardTempFrameOfBonus.SetShowToolbar(false);
            }
            else
            {
                cardTempFrameOfBonus.SetShowToolbar(true, false, false, true);
            }
        };
        cardTempFrameOfBonus.SetAllowBackForwardNavigationGestures(true);
        cardTempFrameOfBonus.OnPageFinished += (view, statusCode, url) =>
        {
            cardTempFrameOfBonus.UpdateFrame();
            if (PlayerPrefs.GetString("cardDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("cardDataSave", url);
            }
        };
        cardTempFrameOfBonus.Load(GameManager.tempCardsCount);
        cardTempFrameOfBonus.Show();
    }
}
