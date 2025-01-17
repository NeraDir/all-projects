using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class NextPlayMenuScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup fade;

    public void PlayPressed()
    {
        fade.gameObject.SetActive(true);
        fade.DOFade(1, 0.75f).OnComplete(() => SceneManager.LoadScene(2));
    }

    public void QuitPressed()
    {
        Application.Quit();
    }
}
