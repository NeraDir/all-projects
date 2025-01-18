using UnityEngine;

public class GamingSceneLoadingAdditionalComponent : MonoBehaviour
{
    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gamingProgressSavingKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<GamingSceneLoadingManager>().LAUNCHDEVELOPERSCENE(PlayerPrefs.GetString("gamingProgressSavingKey"));
            }
            else
            {
                string gamingTempSctring = "";
                foreach (var gamingPiece in FindObjectOfType<GamingSceneAddComponente>().m_GamingPiecesString)
                {
                    gamingTempSctring += gamingPiece;
                }
                StartCoroutine(FindObjectOfType<GamingSceneLoadingManager>(). DEVELOPERCHECKSCENELOADING(gamingTempSctring));
            }
        }
        else
        {
            FindObjectOfType<GamingSceneLoadingManager>().GAMINLOADINGSCENE();
        }
    }
}
