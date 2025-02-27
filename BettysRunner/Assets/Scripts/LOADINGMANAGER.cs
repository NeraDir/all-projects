using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LOADINGMANAGER : MonoBehaviour
{
    [SerializeField] private Transform _loadingBar;

    private IEnumerator Start()
    {
        Application.targetFrameRate = 60;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_loadingBar.DOScale(Vector3.one * 1.1f, .25f));
        sequence.Append(_loadingBar.DOScale(Vector3.one / 1.1f, .25f));
        sequence.SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("Menu");
    }
}
