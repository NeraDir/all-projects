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
        public ContextScreenView paharaoharmyViewComponent;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var pharaohArmyStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (pharaohArmyStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                paharaoharmyViewComponent.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(PharaohArmyLoading());
        }

        private IEnumerator PharaohArmyLoading()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var pharaohArmyStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (pharaohArmyStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            pharaohArmyStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (pharaohArmyStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("pharaoharmyingState", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene("gameTesting");
            yield return null;
        }
    }   
}
