using UnityEngine;
using Facebook.Unity;

public class HandlerFB : MonoBehaviour
{
    void Awake()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            FB.Init(() => {
                FB.ActivateApp();
            });
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
