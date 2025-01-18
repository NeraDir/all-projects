using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public string m_SceneName;

    public float m_SceneLoadTime;

    public bool m_Load;

    public bool m_SimpleLoad;

    [SerializeField]
    private Image m_LoadingFillerImage;

    private float m_LoadCurrentValue;

    private void Start()
    {
        if (m_SimpleLoad)
            StartCoroutine(LoadSceneIEnumerator());
        if (m_Load)
            StartCoroutine(LoadSceneVisual());
    }

    private IEnumerator LoadSceneIEnumerator() 
    {
        yield return new WaitForSeconds(m_SceneLoadTime);
        LoadScene();
    }

    private IEnumerator LoadSceneVisual()
    {
        while(m_LoadCurrentValue < 100) 
        {
            m_LoadCurrentValue = Mathf.MoveTowards(m_LoadCurrentValue,100+1,20 * Time.deltaTime);
            LoadCurrentScene();
            yield return null;
        }
        LoadScene();
    }

    private void LoadCurrentScene() 
    {
        if (m_LoadingFillerImage != null)
            m_LoadingFillerImage.fillAmount = Mathf.Lerp(m_LoadingFillerImage.fillAmount, m_LoadCurrentValue / 100, 11 * Time.deltaTime);
    }

    public void LoadScene() => SceneManager.LoadScene(m_SceneName);

    public void ExitFromGame() => Application.Quit();
}
