using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemi : MonoBehaviour
{
    [SerializeField] private PositionDisposer myPos;
    private Movement movment;
    private Map map;
    private WorldClockSteps clock;
    private List<Vector2Int> path;
    [SerializeField] [Range(1, 4)] private int numState = 1;
    private static List<Vector2Int> nextCells;
    private Vector2Int myNextCell;
    [SerializeField] private int _hp = 1;
    public static int numEnemis;
    private void Start()
    {
        numEnemis++;

        map = Map.instance;
        clock = WorldClockSteps.instance;
        movment = GetComponent<Movement>();

        switch (numState)
        {
            case 1:
                WorldClockSteps.State0 += SearchePath;
                break;
            case 2:
                WorldClockSteps.State1 += SearchePath;
                break;
            case 3:
                WorldClockSteps.State2 += SearchePath;
                break;
            case 4:
                WorldClockSteps.State3 += SearchePath;
                break;
            default:
                Debug.LogError("numState cant be <1 || >4");
                break;
        }
        nextCells = new List<Vector2Int>();
    }
    private void OnDestroy()
    {
        switch (numState)
        {
            case 1:
                WorldClockSteps.State0 -= SearchePath;
                break;
            case 2:
                WorldClockSteps.State1 -= SearchePath;
                break;
            case 3:
                WorldClockSteps.State2 -= SearchePath;
                break;
            case 4:
                WorldClockSteps.State3 -= SearchePath;
                break;
            default:
                Debug.LogError("numState cant be <1 || >4");
                break;
        }
    }
    public void GetDmg(int dmg)
    {
        _hp -= dmg;
        if (_hp <= 0)
            Dead();
    }
    private void Dead()
    {
        numEnemis--;
        MainRoundManager.instance.AddGold(1);
        if (numEnemis <= 0)
            MainRoundManager.instance.Win();

        myPos.DestroyThis();
    }
    private void SearchePath()
    {
        List<Vector2Int> V2i = myPos.GetAroundCells();

        Vector2Int playerposition = CharacterControlling.instance.GetPlayerPositionDisposer().GetMapPosition();

        if (V2i.Contains(playerposition))
        {
            path = new List<Vector2Int>();
            CharacterControlling.instance.GetPlayerPositionDisposer().GetComponent<Player>().Hp(-1);
        }
        else
        {
            nextCells.Remove(myNextCell);
            path = PathSearcher.FindPath(myPos.GetMapPosition(), playerposition, map.map, 100);
            if (path == null || path.Count < 2)
                return;

            if (nextCells.Contains(path[1]))
            {
                movment.SetNextCell(myPos.GetMapPosition());
                return;
            }
            nextCells.Add(path[1]);
            myNextCell = path[1];
            movment.SetNextCell(path[1]);
        }
    }
}
