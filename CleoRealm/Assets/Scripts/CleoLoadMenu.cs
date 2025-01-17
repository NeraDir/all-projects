using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CleoLoadMenu : MonoBehaviour
{
    public static string cleoDataSet;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("CleoMenus");
    }
}
