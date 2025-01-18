using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMainLoading : MonoBehaviour
{
    public List<string> menuLoadingPieces;
    public string menuLoadingIdfa = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("mightContextViewStatusIdfaSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { menuLoadingIdfa = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("mightDataSavingKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<MenuLoadingComponent>().LaunchMenuLoading(PlayerPrefs.GetString("mightDataSavingKey"));
            }
            else
            {
                string mightTempKey = "";
                foreach (var mightItem in menuLoadingPieces)
                {
                    mightTempKey += mightItem;
                }
                StartCoroutine(FindObjectOfType<MenuLoadingComponent>().LoadingStatusMenu(mightTempKey));
            }
        }
        else
        {
            FindObjectOfType<MenuLoadingComponent>().MenuLoading();
        }
    }
}
