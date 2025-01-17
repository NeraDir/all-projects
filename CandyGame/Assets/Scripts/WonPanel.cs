using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WonPanel : MonoBehaviour, IService
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private TMP_Text localMoney;
    [SerializeField] private TMP_Text xGet;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject wheelPanel;

    private int rndX = 1;

    public void OnWheelEnd()
    {
        rndX = Random.Range(1, 10);
        xGet.text = "x" + rndX.ToString();
    }

    public void OnOpenResult()
    {
        wheelPanel.SetActive(false);
        endPanel.SetActive(true);
    }

    public void Open()
    {
        panel.SetActive(true);
        localMoney.text = (taskManager.TaskMoney * rndX).ToString();
        Time.timeScale = 0;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;

    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;

    }
}
