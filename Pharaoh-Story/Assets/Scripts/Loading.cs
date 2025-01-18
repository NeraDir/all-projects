using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public int sceneIndex;

    [SerializeField]
    private TMP_Text texter;

    private float loadingValue;

    private void Start()
    {
        StartCoroutine(Loadinge());
    }

    private IEnumerator Loadinge() 
    {
        while (loadingValue < 100)
        {
            loadingValue = Mathf.Lerp(loadingValue, 101, 2 * Time.deltaTime);
            texter.text = loadingValue.ToString("0") + "%";
            yield return null;
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
