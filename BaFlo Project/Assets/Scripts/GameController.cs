using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private GameState currentGameState;

    [SerializeField]
    private List<EntityController> allEnemyPrefabs;

    public EntityController playerController;
    public EntityController currentEnemyController;

    public EntityInformationModifire playerInfomationDisplayModifire;
    public EntityInformationModifire entityInfomationDisplayModifire;

    public Transform entitiesParentInCanvasScreen;

    public Transform enemySpawnPosPoint;
    public Transform enemyPosInScreenPoint;



    [Header("Gameplay Information")]
    public int levelNumber;
    public int enemyIndex;

    [Header("Default Actions Ref")]
    public AbstarctAction attack;
    public AbstarctAction block;
    [Header("Special Actions Ref")]
    public AbstarctAction fieryRainActionPanel;
    public AbstarctAction poisonRainActionPanel;
    [Header("UI pages")]
    public UI_GamePlayPage uI_GamePlayPage;
    public UI_GameOverPage uI_GameOverPage;
    public UI_LevelCompletedPage uI_LevelCompletedPage;

    [Header("Effects")]
    public GameObject targetLabelIcon;
    public GameObject attackIcon;

    private void OnEnable()
    {
        StartGameManager.StartGameEvent += StartDuel;
    }
    private void OnDisable()
    {
        StartGameManager.StartGameEvent -= StartDuel;

        if (currentGameState != null)
            currentGameState.ExitState();
    }


    private void Start()
    {
        enemyIndex = 0;
        Init();
        //ChangeState(new StartFight());
    }

    private void Init()
    {
        playerController.GetEntityInformation().maxHealthValue += (GamePlayConfigs.healthLevelNumber - 1) * 2;
        playerController.GetEntityInformation().HealthValue = playerController.GetEntityInformation().maxHealthValue;
        playerController.GetEntityInformation().maxEnergyValue += (GamePlayConfigs.energyLevelNumber - 1) * 2;
        playerController.GetEntityInformation().EnergyValue = playerController.GetEntityInformation().maxEnergyValue;
        playerController.GetEntityInformation().damageValue += (GamePlayConfigs.damageLevelNumber - 1) * 2;
    }

    private void StartDuel()
    {
        ChangeState(new StartFight(), 0.1f);
    }

    private void FixedUpdate()
    {
        if (currentGameState != null)
            currentGameState.StateAction();
        
    }

    public void ChangeState(GameState nextState, float timeToChangeState)
    {
        StartCoroutine(changeState(nextState, timeToChangeState));

    }

    private IEnumerator changeState(GameState nextState, float timeToChangeState)
    {
        yield return new WaitForSeconds(timeToChangeState);

        if (currentGameState != null)
        {
            currentGameState.ExitState();
        }

        currentGameState = nextState;
        currentGameState.EnterState(this);

    }

    public List<EntityController> GetAllEnemyPrefabs()
    {
        return allEnemyPrefabs;
    }

    public EntityController SpawnEnemy(EntityController enemyController)
    {
        EntityController result = Instantiate(enemyController, entitiesParentInCanvasScreen);
        return result;
    }

    public float GetPriceForAction(ActionButtonTypes actionButtonTypes)
    {
        AbstarctAction buff = null;

        if (actionButtonTypes == ActionButtonTypes.Attack)
        {
            buff = attack;
            return buff.GetActionPrice();
        }
        else if (actionButtonTypes == ActionButtonTypes.Block)
        {
            return 0;
        }
        else if (actionButtonTypes == ActionButtonTypes.FieryRain)
        {
            return fieryRainActionPanel.GetActionPrice();
        }
        else if (actionButtonTypes == ActionButtonTypes.PoisonRain)
        {
            return poisonRainActionPanel.GetActionPrice();
        }


        return 0;
    }

}
