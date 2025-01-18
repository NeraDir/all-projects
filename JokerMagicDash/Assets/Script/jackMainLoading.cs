using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;


public class jackMainLoading : MonoBehaviour
{
    public List<string> jackLoadingStringers;   

    private void Awake()
    {
        FindObjectOfType<jackAdditionalHelpLoader>().GetIdfaInfo();
        Permission.RequestUserPermission(Permission.Camera);
    }

    private void Start()
    {
        StartCoroutine(Starting());
    }

    private IEnumerator Starting()
    {
        yield return new WaitForSeconds(5);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string tempDataString = PlayerPrefs.GetString("JackGameDataString", string.Empty);
            if (tempDataString != string.Empty)
            {
                var dataBuffered = long.Parse(tempDataString);
                if (dataBuffered >= TimeUtility.SetTimeUtility())
                {
                    FindObjectOfType<jackAdditionalHelpLoader>().LoadGameScene();
                    yield break;
                }
            }

            string tempData = "";
            foreach (string stringItem in jackLoadingStringers)
            {
                tempData += stringItem;
            }

            StartCoroutine(FindObjectOfType<jackViewPanelComponent>().LaunchInitialization(tempData));
        }
        else
        {
            FindObjectOfType<jackAdditionalHelpLoader>().LoadGameScene();
        }
    }
}