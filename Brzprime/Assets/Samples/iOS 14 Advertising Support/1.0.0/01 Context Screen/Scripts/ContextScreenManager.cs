using System.Collections;
using Unity.Advertisement.IosSupport.Components;
using UnityEngine;
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
        public ContextScreenView egyptianBrzingViewer;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var egyptianBrzingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (egyptianBrzingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                egyptianBrzingViewer.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(egyptianBrzingLoading());
        }

        private IEnumerator egyptianBrzingLoading()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var egyptianBrzingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (egyptianBrzingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            egyptianBrzingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (egyptianBrzingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("egyptianBrzingFPOSAVEKEY", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene("EvolveBtwener");
            yield return null;
        }
    }   
}
