using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenPageController : MonoBehaviour
{
    [SerializeField]
    public GameObject nextPage;
    public OpenPageType openPageType;

    [SerializeField]
    private string nextSceneKey;


    public void CallAnimationComplete()
    {
        if (openPageType == OpenPageType.OpenNextPanel)
        {
            nextPage.SetActive(true);
            //gameObject.SetActive(false);
        }
        if (openPageType == OpenPageType.OpenAndDestoy)
        {
            //nextPage.SetActive(true);
            gameObject.SetActive(false);
        }
        if(openPageType == OpenPageType.LoadNextScene)
        {
            SceneManager.LoadScene(nextSceneKey);
        }
    }
}

public enum OpenPageType
{
    LoadNextScene,
    OpenAndDestoy,
    OpenNextPanel
}
