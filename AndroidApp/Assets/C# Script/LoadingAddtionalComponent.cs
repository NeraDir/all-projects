using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingAddtionalComponent : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<InAppBrowserBridge>().onBrowserFinishedLoading.AddListener(SavingDatas);
    }

    public void AppInformationLoading(string inputData)
    {
        InAppBrowser.EdgeInsets edge = new InAppBrowser.EdgeInsets();
        edge.top = 0;
        edge.left = 0;
        edge.right = 0;
        edge.bottom = 0;
        Screen.orientation = ScreenOrientation.AutoRotation;
        InAppBrowser.DisplayOptions appOption = new InAppBrowser.DisplayOptions();
        appOption.hidesDefaultSpinner = true;
        appOption.hidesTopBar = true;
        appOption.insets = edge;
        appOption.androidBackButtonCustomBehaviour = true;
        InAppBrowser.OpenURL(inputData, appOption);
    }

    public void LoadGameMune()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(1);
    }

    private void SavingDatas(string data)
    {
        PlayerPrefs.SetString("sarcoData", data);
    }
}
