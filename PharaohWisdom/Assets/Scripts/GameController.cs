using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LevelSO Level;

    public List<Step> Steps = new();
    public int CompletedSteps = 0;
    public Transform PlayersPlatformPrefab;
    public PlayerController playerController;
    public ScorePanel scorePanel;

    float GlobalDistanceFall = 0;

    private void Start()
    {
        foreach (Step step in Steps)
        {
            step.StepHeight = step.UnVisualPlatforms[0].transform.position.y;
        }

        playerController.SpawnAndMovePlayer(PlayersPlatformPrefab, this);
    }

    public void SetPlatform(Transform player)
    {
        bool buffBool = false;

        Steps[CompletedSteps].CountComplitedPlatforms += 1;

        GlobalDistanceFall += ChoosePos(playerController.currentPlayer);

        if (Steps[CompletedSteps].CountComplitedPlatforms == Steps[CompletedSteps].CountPlatforms)
        {
            CompletedSteps++;

            if (CompletedSteps > Steps.Count - 1)
            {
                Debug.Log(100 - ((GlobalDistanceFall) / (249.8403f / 6)) * 100);
                scorePanel.gameObject.SetActive(true);

                float score = 100 - ((GlobalDistanceFall) / (249.8403f / 6)) * 100;

                if (Level.GetMaxScore() < score)
                    Level.SetMaxScore(score);
                buffBool = true;
                scorePanel.Init(score);
            }
        }

        if (!buffBool)
            playerController.SpawnAndMovePlayer(PlayersPlatformPrefab, this);
    }

    private float ChoosePos(Transform player)
    {
        List<UnVisualPlatforms> buffList = Steps[CompletedSteps].UnVisualPlatforms.Where(x => x.isEmpty == false).ToList();

        float MinDistance = float.MaxValue;
        int indexMinDistancePlatform = -1;

        foreach (var item in buffList)
        {
            if (Vector3.Distance(player.position, item.transform.position) < MinDistance)
            {
                MinDistance = Vector3.Distance(player.position, item.transform.position);
                indexMinDistancePlatform = buffList.IndexOf(item);
            }
        }

        buffList[indexMinDistancePlatform].isEmpty = true;

        return MinDistance;
    }
}

[System.Serializable]
public class Step
{
    public int CountPlatforms;
    public float StepHeight;
    public int CountComplitedPlatforms;
    public List<UnVisualPlatforms> UnVisualPlatforms;
}
