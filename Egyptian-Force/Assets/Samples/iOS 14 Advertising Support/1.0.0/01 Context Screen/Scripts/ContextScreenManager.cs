using System.Collections;
using Unity.Advertisement.IosSupport.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.Advertisement.IosSupport.Samples
{
    public class ContextScreenManager : MonoBehaviour
    {
        public ContextScreenView EgyptPrefab;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var brazinorealStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (brazinorealStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                EgyptPrefab.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(loaderByEgypt());
        }

        private IEnumerator loaderByEgypt()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var EgyptStatusOF = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (EgyptStatusOF == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            EgyptStatusOF = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (EgyptStatusOF == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("egyptBufferSCC", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene("AppWinGame");
            yield return null;
        }
    }   
}
