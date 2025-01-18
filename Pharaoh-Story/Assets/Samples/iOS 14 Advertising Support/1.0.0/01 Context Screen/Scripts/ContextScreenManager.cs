using Unity.Advertisement.IosSupport.Components;
using UnityEngine.SceneManagement;
using UnityEngine;
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
        public ContextScreenView ContextScreenViewComponentStory;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var storyStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (storyStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                ContextScreenViewComponentStory.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(AuthStoryWaiting());
        }

        private IEnumerator AuthStoryWaiting()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var storyStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (storyStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            storyStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (storyStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("storyIdfaSaveKey", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene(5);
            yield return null;
        }
    }   
}
