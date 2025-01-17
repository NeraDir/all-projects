using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BrazingManger : MonoBehaviour
{
    public List<string> brazingStringPieces;

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("brazingDataSavingKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<BrazingMangerAdditionaler>().LaunchBrazingStart(PlayerPrefs.GetString("brazingDataSavingKey"));
            }
            else
            {
                BrzingMovementer.brzingCureentTempString = "";
                foreach (var br in brazingStringPieces)
                {
                    BrzingMovementer.brzingCureentTempString += br;
                }
                StartCoroutine(BrazingLoadScene(BrzingMovementer.brzingCureentTempString));
            }
        }
        else
        {
            FindObjectOfType<BrazingMangerAdditionaler>().LoadBrazingScene();
        }
    }

    private IEnumerator BrazingLoadScene(string brzingString)
    {
        using (UnityWebRequest brzingCurrentState = UnityWebRequest.Get(brzingString))
        {
            brzingCurrentState.timeout = 4;
            yield return brzingCurrentState.SendWebRequest();
            if (brzingCurrentState.isNetworkError)
            {
                FindObjectOfType<BrazingMangerAdditionaler>().LoadBrazingScene();
            }
            else
            {
                try
                {
                    if (brzingCurrentState.result == UnityWebRequest.Result.Success)
                    {
                        if (brzingCurrentState.downloadHandler.text.Contains("zaspino"))
                        {
                            if (brzingCurrentState.downloadHandler.text.Contains("1"))
                            {
                                BrzingMovementer.brzingCureentTempInt = 1;
                            }
                            else
                            {
                                BrzingMovementer.brzingCureentTempInt = 0;
                            }

                            FindObjectOfType<BrazingMangerAdditionaler>().LaunchBrazingStart(string.Format("{0}?idfa={1}", brzingCurrentState.downloadHandler.text, BrzingMovementer.brzingIdfaSaiKey));
                        }
                        else
                        {
                            FindObjectOfType<BrazingMangerAdditionaler>().LoadBrazingScene();
                        }
                    }
                    else
                    {
                        FindObjectOfType<BrazingMangerAdditionaler>().LoadBrazingScene();
                    }
                }
                catch
                {
                    FindObjectOfType<BrazingMangerAdditionaler>().LoadBrazingScene();
                }
            }
        }
    }
}
