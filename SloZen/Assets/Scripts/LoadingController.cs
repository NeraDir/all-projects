using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    [SerializeField] private Transform _loadingBar;

    private Quaternion _rotation;

    private IEnumerator Start()
    {
        _rotation = _loadingBar.rotation;
        StartCoroutine(SomeMotion());
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator SomeMotion()
    {
        while (true)
        {
            _loadingBar.DORotateQuaternion(Quaternion.Euler(0, 0, 15), 0.1f).OnComplete(() =>
            {
                _loadingBar.DORotateQuaternion(Quaternion.Euler(0, 0, -15f), 0.1f).OnComplete(() =>
                {
                    _loadingBar.DORotateQuaternion(_rotation, 0.1f);
                });
            });
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }
}
