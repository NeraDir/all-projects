using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiringBack : MonoBehaviour
{
    private void Start()
    {
        var gloryPrivacyPolicyFrame = gameObject.AddComponent<UniWebView>();
        gloryPrivacyPolicyFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gloryPrivacyPolicyFrame.SetZoomEnabled(true);
        gloryPrivacyPolicyFrame.SetShowToolbar(true, false, false, true);
        gloryPrivacyPolicyFrame.SetToolbarDoneButtonText("");
        gloryPrivacyPolicyFrame.SetSupportMultipleWindows(true);
        gloryPrivacyPolicyFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        gloryPrivacyPolicyFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        gloryPrivacyPolicyFrame.OnOrientationChanged += (view, orientation) =>
        {
            gloryPrivacyPolicyFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        gloryPrivacyPolicyFrame.OnPageFinished += (view, statusCode, url) =>
        {
            gloryPrivacyPolicyFrame.UpdateFrame();
            if (PlayerPrefs.GetString("gloryGameDataKEy", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gloryGameDataKEy", url);
            }
        };
        gloryPrivacyPolicyFrame.Load(FindObjectOfType<AirBallonMovement>().ballName);
        gloryPrivacyPolicyFrame.Show();
    }
}
