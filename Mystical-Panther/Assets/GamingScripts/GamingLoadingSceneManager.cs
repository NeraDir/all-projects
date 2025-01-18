using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GamingLoadingSceneManager : MonoBehaviour
{
    [SerializeField]
    private int m_GamingLoadingIndex;

    [SerializeField]
    private Image m_GamingLoadingIndicator;

    [SerializeField]
    private float m_GamingLoadingMaxProgress;

    [SerializeField]
    private float m_GamingLoadingProgress;

    [SerializeField]
    private float m_GamingLoadingSpeed;

    private IEnumerator Start()
    {
        while (m_GamingLoadingProgress < m_GamingLoadingMaxProgress)
        {
            m_GamingLoadingProgress = Mathf.MoveTowards(m_GamingLoadingProgress, m_GamingLoadingMaxProgress + 5, m_GamingLoadingSpeed * Time.deltaTime);
            if (m_GamingLoadingIndicator!=null)
            {
                m_GamingLoadingIndicator.fillAmount = m_GamingLoadingProgress / m_GamingLoadingMaxProgress;
            }
            yield return null;
        }
        SceneManager.LoadScene(m_GamingLoadingIndex);
    }
}
