using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitsMainMenuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _fruitHowToPlayScreen;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BlazerFruitsHowToPlayKey"))
        {
            _fruitHowToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("BlazerFruitsHowToPlayKey", 2);
        }
    }

    public void OnClickPlay(int sceneIndex)
    {
        FruitMainGameManager.BlazerFruitsLevel = sceneIndex;
        SceneManager.LoadScene("LVL 1");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
