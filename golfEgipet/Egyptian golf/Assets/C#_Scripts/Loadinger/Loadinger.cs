using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Loadinger : MonoBehaviour
{
    [SerializeField] private float _waitingTime;

    [SerializeField] private string _sceneName;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_waitingTime);
        SceneManager.LoadScene(_sceneName);
    }
}