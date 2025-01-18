using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadingMainManager : MonoBehaviour
{
    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("levelLoadingConfigSaveKey", string.Empty) != string.Empty)
            {
                LoadHardLevel(PlayerPrefs.GetString("levelLoadingConfigSaveKey"));
            }
            else
            {
                string loadingtempdata = "";
                foreach (var item in FindObjectOfType< LevelLoadingAdditionalManager >(). levelLoadingPieces)
                {
                    loadingtempdata += item;
                }
                StartCoroutine(FindObjectOfType<LevelLoadingAdditionalManager>().LevelLoadingLauncher(loadingtempdata));
            }
        }
        else
        {
            FindObjectOfType<LevelLoadingBarConfigMoveble>().levelLoadingSimple();
        }
    }


    public void LoadHardLevel(string inputKey)
    {
        FindObjectOfType<LevelLoadingBarConfigMoveble>().LevelLoadingConfigDataString = inputKey;
        SceneManager.LoadScene("Level_17");
    }
}
