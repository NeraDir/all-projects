using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_LoadingPage : MonoBehaviour
{

    [SerializeField]
    private Slider loadSlider;


    private float loadDuration = 2f;
    private float elapsedTime = 0;
    private string menuSceneKey = "Menu";


    private void OnEnable()
    {
        loadSlider.value = 0;
        StartCoroutine(load());
    }

    private IEnumerator load()
    {
        while (elapsedTime < loadDuration)
        {
            loadSlider.value = Mathf.Lerp(0, 1, elapsedTime / loadDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        LoadMenuScene();
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneKey);
    }
}
