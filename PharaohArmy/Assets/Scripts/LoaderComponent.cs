using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderComponent : MonoBehaviour
{
    [SerializeField]
    private Image loadingBar;

    [SerializeField]
    private float loadingTimer;

    private float currentTimer;

    [SerializeField]
    private int sceneIndex;

    private IEnumerator Start()
    {
        while (currentTimer<loadingTimer)
        {
            yield return null;
            currentTimer = Mathf.MoveTowards(currentTimer, loadingTimer + 10, 1 * Time.deltaTime);
            UpdateProgressBar();
        }
        SceneManager.LoadScene(sceneIndex);
    }

    private void UpdateProgressBar() 
    {
        if (loadingBar != null)
            loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, currentTimer / loadingTimer, 10 * Time.deltaTime);
    }
}
