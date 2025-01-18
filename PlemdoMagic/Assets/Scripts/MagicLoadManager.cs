using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicLoadManager : MonoBehaviour
{
    public string magicLoadScene;

    public float magicLoadSceneTime;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(magicLoadSceneTime);
        SceneManager.LoadScene(magicLoadScene);
    }
}
