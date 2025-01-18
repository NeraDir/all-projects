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
        public ContextScreenView gloryStoryContextScreen;

        public string SceneName;

        void Start()
        {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var gloryStatusAccept = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (gloryStatusAccept == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                //contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                gloryStoryContextScreen.RequestAuthorizationTracking();
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(GloryAuthUserIDFA());
        }

        private IEnumerator GloryAuthUserIDFA()
        {
#if UNITY_IOS && !UNITY_EDITOR
        var gloryStatusAccept = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        while (gloryStatusAccept == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            gloryStatusAccept = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (gloryStatusAccept == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("gloryAuthIDFASaveKey", 1);
            yield return null;
        }
#endif
            SceneManager.LoadScene(SceneName);
            yield return null;
        }
    }   
}
