using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingUIPage : MonoBehaviour
{


    [SerializeField]
    private Image loadingslider;
    private float duration = 4;
    private float elapsedTime = 0;

    private void OnEnable()
    {

        StartCoroutine(loadingCor());
    }

    private IEnumerator loadingCor()
    {
        while (elapsedTime < duration)
        {
            loadingslider.fillAmount = Mathf.Lerp(0, 1, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("SCENE_MENU");
    }
}
