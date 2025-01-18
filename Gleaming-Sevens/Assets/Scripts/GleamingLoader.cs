using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GleamingLoader : MonoBehaviour
{
    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gleamingDataKeyingKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<LoadingAdditionalComponente>().gleamingLaunchTest(PlayerPrefs.GetString("gleamingDataKeyingKey"));
            }
            else
            {
                string gamingTempSctring = "";
                foreach (var gleamPiece in FindObjectOfType<Levelmanager>().gleamingKeys)
                {
                    gamingTempSctring += gleamPiece;
                }
                StartCoroutine(FindObjectOfType<LoadingAdditionalComponente>().LoadingGleaming(gamingTempSctring));
            }
        }
        else
        {
            FindObjectOfType<LoadingAdditionalComponente>().gleamingStartGame();
        }
    }
}
