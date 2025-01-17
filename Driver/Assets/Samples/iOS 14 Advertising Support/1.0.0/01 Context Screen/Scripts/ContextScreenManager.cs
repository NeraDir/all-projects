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
        public ContextScreenView brazingViewComponent;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var brazingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (brazingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                brazingViewComponent.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(BrazingLoading());
        }

        private IEnumerator BrazingLoading()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var brazingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (brazingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            brazingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (brazingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("brazingGameDataSavekey", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene(2);
            yield return null;
        }
    }   
}
