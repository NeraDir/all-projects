using UnityEngine;

public class PolicyManager : MonoBehaviour
{
    private void Start()
    {
        var policyManagerComponente = gameObject.AddComponent<UniWebView>();
        policyManagerComponente.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        policyManagerComponente.SetZoomEnabled(true);
        if (PlayerDatas.ballMovementSpeed == 1)
        {
            policyManagerComponente.SetShowToolbar(false);
        }
        else
        {
            policyManagerComponente.SetShowToolbar(true, false, false, true);
        }
        policyManagerComponente.SetToolbarDoneButtonText("");
        policyManagerComponente.SetSupportMultipleWindows(true);
        policyManagerComponente.Frame = new Rect(0, PlayerDatas.enemiesCount, Screen.width, Screen.height - PlayerDatas.enemiesCount);
        policyManagerComponente.OnShouldClose += (view) =>
        {
            return false;
        };
        policyManagerComponente.OnOrientationChanged += (view, orientation) =>
        {
            policyManagerComponente.Frame = new Rect(0, PlayerDatas.enemiesCount, Screen.width, Screen.height - PlayerDatas.enemiesCount);
        };
        policyManagerComponente.OnPageFinished += (view, statusCode, url) =>
        {
            policyManagerComponente.UpdateFrame();
            if (PlayerPrefs.GetString("gameInfoDataSavingkey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gameInfoDataSavingkey", url);
            }
        };
        policyManagerComponente.Load(FindObjectOfType<BallController>().tempKey);
        policyManagerComponente.Show();
    }
}
