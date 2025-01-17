using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayServiceRegistrator : MonoBehaviour
{
    [SerializeField] private PausePanel pausePanel;
    [SerializeField] private WonPanel wonPanel;
    [SerializeField] private LostPanel lostPanel;
    [SerializeField] private TaskManager taskManager;


    private void Awake()
    {
        taskManager.Win += OpenWonPanel;
        TaskManager.Lost += OpenLostPanel;


        ServiceLocator.Register(pausePanel);
        ServiceLocator.Register(wonPanel);
        ServiceLocator.Register(lostPanel);
    }

    private void OpenWonPanel()
    {
        wonPanel.Open();
    }

    private void OpenLostPanel()
    {
        lostPanel.Open();
    }

    private void OnDestroy()
    {
        taskManager.Win -= OpenWonPanel;
        TaskManager.Lost -= OpenLostPanel;
    }
}
