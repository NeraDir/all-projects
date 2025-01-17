
using Unity.Advertisement.IosSupport.Components;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Unity.Advertisement.IosSupport.Samples
{
    /// <summary>
    /// This component will trigger the context screen to appear when the scene starts,
    /// if the user hasn't already responded to the iOS tracking dialog.
    /// </summary>
    public class ContextScreenManager : MonoBehaviour
    {
        /// <summary>
        /// The prefab that will be instantiated by this component.
        /// The prefab has to have an ContextScreenView component on its root GameObject.
        /// </summary>
        public ContextScreenView JellyPeaksContextPrefab;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var viewTrackingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (viewTrackingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                JellyPeaksContextPrefab.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(JellyStartCoroutine());
        }

        private IEnumerator JellyStartCoroutine()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var viewTrackingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (viewTrackingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            viewTrackingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (viewTrackingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("PlayerPrefsJellyPeaksIdfaDataKey", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene("JellyPeaks_MENU_GAME_INIT");
            yield return null;
        }
    }   
}
