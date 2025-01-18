using System.Collections;
using Unity.Advertisement.IosSupport.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.Advertisement.IosSupport.Samples
{
    /// <summary>
    /// This component will trigger the contextInfopimoprodigyIdfaDataKey screen to appear when the scene starts,
    /// if the user hasn't already responded to the iOS tracking dialog.
    /// </summary>
    public class ContextScreenManager : MonoBehaviour
    {
        /// <summary>
        /// The prefab that will be instantiated by this component.
        /// The prefab has to have an ContextScreenView component on its root GameObject.
        /// </summary>
        public ContextScreenView CoontextScreenViewComponent;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var coontextviewStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (coontextviewStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //coontextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextInfopimoprodigyIdfaDataKeyScreen.gameObject);
                CoontextScreenViewComponent.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(OnPimoProdigyMethod());
        }

        private IEnumerator OnPimoProdigyMethod()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var coontextviewStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (coontextviewStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            coontextviewStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (coontextviewStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("contextInfopimoprodigyIdfaDataKey", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene("MainScene");
            yield return null;
        }
    }
}
