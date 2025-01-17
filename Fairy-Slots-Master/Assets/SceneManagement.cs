using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField]
    private string sceneKeyString;
    [SerializeField]
    private float switchStateTime;


    private void OnEnable()
    {
        StartCoroutine(setScenesState());
    }

    public IEnumerator setScenesState()
    {

        yield return new WaitForSeconds(switchStateTime);
        SceneManager.LoadScene(sceneKeyString);
    }
}
