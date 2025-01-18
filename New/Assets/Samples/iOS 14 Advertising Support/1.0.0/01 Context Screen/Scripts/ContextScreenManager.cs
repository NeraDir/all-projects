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
        public ContextScreenView seveningContexterViewComponent;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var seveningStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (seveningStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                seveningContexterViewComponent.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(SeveningLoad());
        }

        private IEnumerator SeveningLoad()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var seveningStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (seveningStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            seveningStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (seveningStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("seveningIdfaSaveKey", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene("LoadingScene");
            yield return null;
        }
    }   
}
