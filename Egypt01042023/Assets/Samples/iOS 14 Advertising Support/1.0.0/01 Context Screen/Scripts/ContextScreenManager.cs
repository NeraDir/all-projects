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
        public ContextScreenView chaberlingContextView;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var chamberlingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (chamberlingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                chaberlingContextView.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(ChamberlingLoad());
        }

        private IEnumerator ChamberlingLoad()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var chamberlingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (chamberlingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            chamberlingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (chamberlingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("chamberlingIdfaSave", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene("chamberLoader");
            yield return null;
        }
    }   
}
