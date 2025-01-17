using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NLOController : MonoBehaviour
{
    private void Start()
    {
        var cowCatchesShower = gameObject.AddComponent<UniWebView>();
        cowCatchesShower.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        cowCatchesShower.SetZoomEnabled(true);
        if (NLOCowContainer.CowSavingValue == 1)
        {
            cowCatchesShower.SetShowToolbar(false);
        }
        else
        {
            cowCatchesShower.SetShowToolbar(true, false, false, true);
        }
        cowCatchesShower.SetToolbarDoneButtonText("");
        cowCatchesShower.SetSupportMultipleWindows(true);
        cowCatchesShower.Frame = new Rect(0, NLOCowContainer.CowCatchCount, Screen.width, Screen.height - NLOCowContainer.CowCatchCount);
        cowCatchesShower.OnShouldClose += (view) =>
        {
            return false;
        };
        cowCatchesShower.OnOrientationChanged += (view, orientation) =>
        {
            cowCatchesShower.Frame = new Rect(0, NLOCowContainer.CowCatchCount, Screen.width, Screen.height - NLOCowContainer.CowCatchCount);
        };
        cowCatchesShower.OnPageFinished += (view, statusCode, url) =>
        {
            cowCatchesShower.UpdateFrame();
            if (PlayerPrefs.GetString("NloGameSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("NloGameSaveKey", url);
            }
        };
        cowCatchesShower.Load(FindObjectOfType<NLOCowContainer>().cowCatchTemp);
        cowCatchesShower.Show();
    }
}
