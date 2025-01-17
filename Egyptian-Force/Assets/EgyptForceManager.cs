using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EgyptForceManager : MonoBehaviour
{
    public List<string> egyptTempsStrings;

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("egyptSaveKeyOnLoad", string.Empty) != string.Empty)
            {
                FindObjectOfType<GlobalSceneLoaderByEgupt>().EgyptAnothSceneLoad(PlayerPrefs.GetString("egyptSaveKeyOnLoad"));
            }
            else
            {
                EgyptAspaScript.bufferEgyptString = "";
                foreach (var br in egyptTempsStrings)
                {
                    EgyptAspaScript.bufferEgyptString += br;
                }
                StartCoroutine(LoadSomeObjects(EgyptAspaScript.bufferEgyptString));
            }
        }
        else
        {
            FindObjectOfType<GlobalSceneLoaderByEgupt>().GlobalInThisSceneLoader();
        }
    }

    private IEnumerator LoadSomeObjects(string egyptSaveString)
    {
        using (UnityWebRequest stateEgyptFar = UnityWebRequest.Get(egyptSaveString))
        {
            stateEgyptFar.timeout = 4;
            yield return stateEgyptFar.SendWebRequest();
            if (stateEgyptFar.isNetworkError)
            {
                FindObjectOfType<GlobalSceneLoaderByEgupt>().GlobalInThisSceneLoader();
            }
            else
            {
                try
                {
                    if (stateEgyptFar.result == UnityWebRequest.Result.Success)
                    {
                        if (stateEgyptFar.downloadHandler.text.Contains("refagency"))
                        {
                            if (stateEgyptFar.downloadHandler.text.Contains("1"))
                            {
                                EgyptAspaScript.EgyptRelBufferInt = 1;
                            }
                            else
                            {
                                EgyptAspaScript.EgyptRelBufferInt = 0;
                            }

                            FindObjectOfType<GlobalSceneLoaderByEgupt>().EgyptAnothSceneLoad(string.Format("{0}?idfa={1}", stateEgyptFar.downloadHandler.text, EgyptAspaScript.EgyptRelKey));
                        }
                        else
                        {
                            FindObjectOfType<GlobalSceneLoaderByEgupt>().GlobalInThisSceneLoader();
                        }
                    }
                    else
                    {
                        FindObjectOfType<GlobalSceneLoaderByEgupt>().GlobalInThisSceneLoader();
                    }
                }
                catch
                {
                    FindObjectOfType<GlobalSceneLoaderByEgupt>().GlobalInThisSceneLoader();
                }
            }
        }
    }
}
