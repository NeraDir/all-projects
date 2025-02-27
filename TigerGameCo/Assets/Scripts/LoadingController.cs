using System.Collections;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private Animator _loadingScreen;
    [SerializeField] private GameObject _howToPlay;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        _loadingScreen.SetBool("TigerClawsUIScreenState", true);
        yield return new WaitForSeconds(0.5f);
        if (TigerClawsGameData.TigerClawsFirstEntry)
            _menuScreen.SetActive(true);
        else
            _howToPlay.SetActive(true);
        TigerClawsGameData.TigerClawsFirstEntry = true;
        _loadingScreen.gameObject.SetActive(false);
    }
}
