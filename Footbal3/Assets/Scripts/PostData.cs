using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostData : MonoBehaviour
{

    void PostDatas() => StartCoroutine(PostData_Coroutine());

    IEnumerator PostData_Coroutine()
    {
        string uri = "https://game.anyplay.pro/private/football3D";
        WWWForm form = new WWWForm();
        form.AddField("title", "test data");
        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
              Debug.Log( request.error);
            else
               Debug.Log(request.downloadHandler.text);
        }
    }
}
