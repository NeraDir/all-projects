using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private Animator m_MenuAnimator;

    public int sceneIndex;

    public void OnCLickOpenGame() 
    {
        StartCoroutine(LoadGameScene());
    }

    public void OnCLickCloseGame() 
    {
        Application.Quit();
    }

    private IEnumerator LoadGameScene()
    {
        m_MenuAnimator.SetBool("UI_ANIMATIONSTATEINDEX", true);
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(sceneIndex);
    }
}
