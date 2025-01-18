using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoadingMaanger : MonoBehaviour
{
    [SerializeField]
    private float _timeOfLoadingGame;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_timeOfLoadingGame);
        SceneManager.LoadScene("MainMenu");
    }
}
