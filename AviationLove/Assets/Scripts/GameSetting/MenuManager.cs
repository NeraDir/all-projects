using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] showScores;

    public GameObject hwp;

    public ReactiveJatpackBuyContainer container;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("AviationHwpSaveKey")) 
        {
            container.BuyAndSelectJatpack();
            hwp.SetActive(true);
            PlayerPrefs.SetString("AviationHwpSaveKey", "firstAdd");
        }
    }

    private void LateUpdate()
    {
        foreach (var item in showScores)
        {
            item.text = AviationDataSaveClass.AviationLoveMoneys.ToString();
        }
    }

    public void OpenGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void CloseGame() 
    {
        Application.Quit();
    }
}
