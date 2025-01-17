using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static int allMoneyValue;
    public static int currenttSpinCount;
    public static float gameTime;

    [SerializeField]
    private Boat boatMainComponent;

    [SerializeField]
    private GameObject _slotPanel;
    [SerializeField]
    private GameObject _gameUIPanel;

    public static bool canSpawnObjects; 

    private void OnEnable()
    {
        boatMainComponent.Init(BoatGameData.boatSpeedLevelNumber);
        allMoneyValue = BoatGameData.allCoinsCount;
        gameTime = BoatGameData.gameTimeLevelNumber * 10;

        currenttSpinCount = 3;
        canSpawnObjects = true;

        LoadGameParameters();

        StartCoroutine(setTimer());
    }


    public void LoadGameParameters()
    {
        int buffValue = 0;

        buffValue = BoatGameData.betValueLevelNumber;
        SlotButtonManager.betVar = 100 + ((BoatGameData.betValueLevelNumber - 1) * 15);

        gameTime = 15 + ((BoatGameData.gameTimeLevelNumber - 1) * 5);





    }

    private IEnumerator setTimer()
    {
        while (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            yield return null;
        }
        _gameUIPanel.SetActive(false);
        _slotPanel.SetActive(true);

        MainGameManager.canSpawnObjects = false;

        boatMainComponent.StopPlayer();
    }
}
