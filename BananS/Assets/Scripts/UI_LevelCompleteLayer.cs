using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelCompleteLayer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> startIcons;

    [SerializeField]
    private bool isGameOverPanel;

    public delegate void TapToAnyBuutonDelegate();
    public static event TapToAnyBuutonDelegate TapToNextLevelBuutonEvent;
    public static event TapToAnyBuutonDelegate TapToRestartLevelBuutonEvent;
    public static event TapToAnyBuutonDelegate TapToMenuBuutonEvent;


    private void OnEnable()
    {
        //Time.timeScale = 1;

        if (isGameOverPanel)
            Invoke(nameof(StopTime), 3f);

        HideAllStarsIcon();
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void HideAllStarsIcon()
    {
        if (isGameOverPanel)
            return;

        for (int i = 0; i < startIcons.Count; i++)
        {
            startIcons[i].SetActive(false);
        }
    }

    public void ShowStarsCount()
    {

        if (isGameOverPanel)
            return;
        else
        {
            int starsCount = 0;

            if (ParametersPerformer.sweetieCount == LevelManager._maxStarsCount)
            {
                starsCount = 3;
            }
            else if (ParametersPerformer.sweetieCount >= LevelManager._maxStarsCount / 2)
            {
                starsCount = 2;
            }
            else if (ParametersPerformer.sweetieCount < LevelManager._maxStarsCount / 2)
            {
                starsCount = 1;
            }


            StartCoroutine(showStarsIcon(starsCount));
        }
    }


    private IEnumerator showStarsIcon(int count)
    { 

        for (int i = 0; i < count; i++)
        {
            startIcons[i].SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }

    public void NextLevelButtonFunction()
    {
        if (TapToNextLevelBuutonEvent != null)
            TapToNextLevelBuutonEvent();
    }
    public void RestartLevelButtonfunction()
    {
        if (TapToRestartLevelBuutonEvent != null)
            TapToRestartLevelBuutonEvent();
    }
    public void MenuButtonFunction()
    {
        if (TapToMenuBuutonEvent != null)
            TapToMenuBuutonEvent();
    }

}
