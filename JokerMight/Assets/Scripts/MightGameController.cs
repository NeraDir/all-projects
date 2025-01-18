using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MightGameController : MonoBehaviour
{
    public static string mightGameController;

    public GameObject xoroPage;

    public GameObject mightGameResult;

    public static int mightGameScore;

    public TMP_Text[] mightScoreDisplayers;

    public static int mightHearts;

    public GameObject roadPiecePrefab;

    public Transform roadSpawnPosition;

    public Transform[] mightHeartsImages;

    public List<Transform> roadPieces = new List<Transform>();

    public static bool gameStarted;

    private bool isLooseXorO;

    public static bool botTurn;

    public static float piecesMoveSpeed = 2;

    public List<MightXorOComponent> botCells = new List<MightXorOComponent>();

    public TMP_Text whoTurnShow;

    public TMP_Text showWhoWon;

    public GameObject gameOverPanel;

    public CellsContainers[] cllsContainers;

    private void Start()
    {
        mightHearts = 3;
        isLooseXorO = false;
        piecesMoveSpeed = 2;
        mightGameScore = 0;
        gameStarted = true;
        MightPieceOfRoad.clickedRoad.AddListener(SpawnRoad);
        MightPieceOfRoad.heartsMinusEvent.AddListener(HeartsMinus);
        roadPieces[1].GetComponent<MightPlatformComponent>().isSpawner = true;

    }

    private void OnDestroy()
    {
        MightPieceOfRoad.clickedRoad.RemoveAllListeners();
        MightPieceOfRoad.heartsMinusEvent.RemoveAllListeners();
    }

    private void HeartsMinus() 
    {
        if (mightHearts <= 0)
        {
            if (!isLooseXorO)
            {
                xoroPage.SetActive(true);
                whoTurnShow.text = "YOU Turn";
                isLooseXorO = true;
                StartCoroutine(BotTurning());
            }
            else
            {
                mightGameResult.SetActive(true);
            }
        }
    }

    private void SpawnRoad() 
    {
        Transform tempRoad = Instantiate(roadPiecePrefab.GetComponent<Transform>(), roadSpawnPosition.position, roadSpawnPosition.rotation);
        roadPieces.Add(tempRoad);
        MoveRoad();
    }

    private void LateUpdate()
    {
        foreach (var item in mightScoreDisplayers)
        {
            item.text = mightGameScore.ToString();
        }

        for (int i = 0; i < mightHeartsImages.Length; i++)
        {
            if (i<mightHearts)
            {
                mightHeartsImages[i].DOScale(Vector3.one, 0.25f);
            }
            else
            {
                mightHeartsImages[i].DOScale(Vector3.zero, 0.25f);
            }
        }

        if (mightGameScore > MightMenuComponent.BestScore)
        {
            MightMenuComponent.BestScore = mightGameScore;
        }
    }

    private IEnumerator BotTurning()
    {
        while (true)
        {
            if (botTurn)
            {
                yield return new WaitForSeconds(1);
                botCells[Random.Range(0, botCells.Count)].OnClickBotTurn();
            }
            yield return null;
        }
    }

    public List<MightXorOComponent> GetPlayerWon()
    {
        foreach (var item in cllsContainers)
        {
            if (item.GetWinnerPlayer() != null)
            {
                return item.GetWinnerPlayer();
            }
        }

        return null;
    }

    public List<MightXorOComponent> GetBotWon()
    {
        foreach (var item in cllsContainers)
        {
            if (item.GetWinner() != null)
            {
                return item.GetWinner();
            }
        }

        return null;
    }

    private void MoveRoad() 
    {
        foreach (var item in roadPieces)
        {
            item.DOMoveX(item.transform.position.x - 1.46f, 0.25f);
            item.GetComponent<MightPlatformComponent>().isSpawner = false;
        }
        roadPieces.Remove(roadPieces[0]);
        roadPieces[1].GetComponent<MightPlatformComponent>().isSpawner = true;
        piecesMoveSpeed += 1f;
    }

    public void OnClickMenuOpen() 
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnClickRestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

[System.Serializable]
public class CellsContainers
{
    public MightXorOComponent[] cells;
    public List<MightXorOComponent> GetWinner()
    {

        List<MightXorOComponent> tempBotCells = new List<MightXorOComponent>();

        foreach (var item in cells)
        {
            if (item.isBotTurned && !item.isPlayerTuned)
            {
                tempBotCells.Add(item);
            }
        }

        if (tempBotCells.Count >= 3)
        {
            return tempBotCells;
        }

        return null;
    }

    public List<MightXorOComponent> GetWinnerPlayer()
    {
        List<MightXorOComponent> tempPlayerCells = new List<MightXorOComponent>();
        foreach (var item in cells)
        {
            if (item.isPlayerTuned && !item.isBotTurned)
            {
                tempPlayerCells.Add(item);
            }
        }

        if (tempPlayerCells.Count >= 3)
        {
            return tempPlayerCells;
        }

        return null;
    }
}
