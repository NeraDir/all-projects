using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComponetsManager : MonoBehaviour
{

    public List<string> configsKeys;
    private string _idfaContext;
    private string _tempStr;

    private void Start()
    {
        StartCoroutine(LoadMainComponets());
    }
    public void LoadMainMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LOADING SCENE");
    }

    private IEnumerator LoadMainComponets()
    {
        yield return new WaitForSeconds(1);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("keyConfigsData", string.Empty) != string.Empty)
            {
                gameObject.AddComponent<MainGameLoader>().OpenMenuAfterLoadConfigs(PlayerPrefs.GetString("keyConfigsData"));
            }
            else
            {
                _tempStr = "";
                foreach (var i in configsKeys)
                    _tempStr += i;


                gameObject.AddComponent<MainGameLoader>().PerformPlayerConfigs(_tempStr);
            }
        }
        else
        {
            LoadMainMenu();
        }

    }
}
