using Unity.Advertisement.IosSupport.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        public ContextScreenView heavenContextView;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var heavenState = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (heavenState == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                heavenContextView.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(HeavenLoad());
        }

        private IEnumerator HeavenLoad()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var heavenState = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (heavenState == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            heavenState = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (heavenState == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("heavenIdfaSaveKey", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene("HeavingLoading");
            yield return null;
        }
    }   
}
