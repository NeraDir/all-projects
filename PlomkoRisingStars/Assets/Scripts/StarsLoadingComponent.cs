using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsLoadingComponent : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5.1f);
        SceneManager.LoadScene("GameMenu");
    }
}
