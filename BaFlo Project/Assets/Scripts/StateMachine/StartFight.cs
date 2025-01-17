using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFight : GameState
{
    private GameController parent;

    private EntityController enemyController;

    private Transform enemySpawnPosPoint;
    private Transform enemyPosInScreenPoint;

    private float enemyTransformSpeed;

    private int enemyIndex;

    private bool canTransformEnemy;

    public override void EnterState(GameController gameController)
    {
        parent = gameController;

        canTransformEnemy = false;

        parent.enemyIndex++;
        enemyIndex = parent.enemyIndex - 1;

        enemyTransformSpeed = 10f;

        this.enemySpawnPosPoint = parent.enemySpawnPosPoint;
        this.enemyPosInScreenPoint = parent.enemyPosInScreenPoint;
        SpawnEnemy();

        if (!parent.playerInfomationDisplayModifire.gameObject.activeInHierarchy)
        {
            parent.playerInfomationDisplayModifire.gameObject.SetActive(true);
        }

        int currentGameLevel = GamePlayConfigs.levelNumber;
        int currrentEnemyLevel = 0;

        if (currentGameLevel - 1 != 0)
        {
            currrentEnemyLevel = Random.Range(currentGameLevel - 1, currentGameLevel + 1);
        }
        else
        {
            currrentEnemyLevel = 1;
        }

        enemyController.GetEntityInformation().LevelNumber = currrentEnemyLevel;
       

        parent.playerInfomationDisplayModifire.SetInfo(parent.playerController.GetEntityInformation());
        parent.entityInfomationDisplayModifire.SetInfo(enemyController.GetEntityInformation());

    }

    public override void ExitState()
    {
        if (enemyController != null)
        {
            enemyController.SetRival(parent.playerController);
            parent.currentEnemyController = enemyController;
        }

        if (parent.playerController != null) 
            parent.playerController.SetRival(parent.currentEnemyController);
        
    }

    public override void StateAction()
    {
        if (canTransformEnemy == true)
        {
            if (enemyController.transform.position != enemyPosInScreenPoint.position)
            {
                enemyController.transform.position = Vector3.MoveTowards(enemyController.transform.position, enemyPosInScreenPoint.position, enemyTransformSpeed);
            }
            else
            {
                canTransformEnemy = false;
                //parent.ChangeState(new WaintPlayerAction());
                parent.entityInfomationDisplayModifire.gameObject.SetActive(true);
                parent.ChangeState(new WaintPlayerAction(), 0.1f);
            }
        }
    }


    private void SpawnEnemy()
    {

        enemyController = parent.SpawnEnemy(GetCurrectEnemy());
        enemyController.transform.position = enemySpawnPosPoint.position;
        //enemySpawnPosPoint.position, enemySpawnPosPoint.rotation, parent.entitiesParentInCanvasScreen);
        canTransformEnemy = true;
        //StartCoroutine(ShowEnemy());
    }

    private EntityController GetCurrectEnemy()
    {
        return parent.GetAllEnemyPrefabs()[enemyIndex];
    }
    
}

