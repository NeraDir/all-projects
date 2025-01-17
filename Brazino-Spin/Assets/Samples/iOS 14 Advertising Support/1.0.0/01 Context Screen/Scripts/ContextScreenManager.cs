using System.Collections;
using Unity.Advertisement.IosSupport.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.Advertisement.IosSupport.Samples
{
    public class ContextScreenManager : MonoBehaviour
    {
        public ContextScreenView BrazContPrefab;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var brazinorealStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (brazinorealStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                BrazContPrefab.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(LoginBrazino());
        }

        private IEnumerator LoginBrazino()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var brazinorealStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (brazinorealStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            brazinorealStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (brazinorealStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("brazinoOk", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene("AppPlayScene");
            yield return null;
        }
    }   
}
