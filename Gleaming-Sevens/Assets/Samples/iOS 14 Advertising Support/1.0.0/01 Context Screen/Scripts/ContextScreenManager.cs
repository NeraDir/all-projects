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
        public ContextScreenView contextScreenPrefab;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var glemSevenSt = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (glemSevenSt == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                contextScreenPrefab.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(StaringLoading());
        }

        private IEnumerator StaringLoading()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var glemSevenSt = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (glemSevenSt == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            glemSevenSt = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (glemSevenSt == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("glemSevIdfakey", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene("AppGS");
            yield return null;
        }
    }   
}
