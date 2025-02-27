using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicCrazTideLoadingManager : MonoBehaviour
{
    [SerializeField] private MagicCrazBetweenComponent _betweener;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(8);
        _betweener.gameObject.SetActive(true);
        _betweener.action = OnLoadEnd;
    }

    private void OnLoadEnd()
    {
        SceneManager.LoadScene("Menu");
    }
}
