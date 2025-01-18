using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 1.5f;

    [SerializeField] [Range(1, 4)] private int numState = 1;

    private float speed;
    private Vector2Int oldCellToStandOn;
    private bool moving = false;
    private Map map;
    private WorldClockSteps clock;
    private PositionDisposer pos;
    private Vector2Int newcellToStandOn;


    private void Awake()
    {
        if (_speed <= 0)
            Debug.LogError("speed = 0");
        pos = GetComponent<PositionDisposer>();
    }
    private void Start()
    {
        speed = _speed;
        map = Map.instance;
        clock = WorldClockSteps.instance;
        switch (numState)
        {
            case 1:
                WorldClockSteps.State0 += Move;
                break;
            case 2:
                WorldClockSteps.State1 += Move;
                break;
            case 3:
                WorldClockSteps.State2 += Move;
                break;
            case 4:
                WorldClockSteps.State3 += Move;
                break;
            default:
                Debug.LogError("numState cant be <1 || >4");
                break;

        }
    }
    private void OnDestroy()
    {
        switch (numState)
        {
            case 1:
                WorldClockSteps.State0 -= Move;
                break;
            case 2:

                WorldClockSteps.State1 -= Move;
                break;
            case 3:
                WorldClockSteps.State2 -= Move;
                break;
            case 4:
                WorldClockSteps.State3 -= Move;
                break;
            default:
                Debug.LogError("numState cant be <1 || >4");
                break;
        }
    }
    private void FixedUpdate()
    {
        if (!moving)
            return;

        transform.position = Vector2.MoveTowards(transform.position, map.map[newcellToStandOn].transform.position, speed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, map.map[newcellToStandOn].transform.position) < 0.00001f)
        {
            transform.position = map.map[newcellToStandOn].transform.position;
            pos.AttachMapPosition();
            moving = false;
        }
    }
    private void Move()
    {
        if (newcellToStandOn == null || newcellToStandOn == oldCellToStandOn)
            return;

        if (Vector2.Distance(transform.position, map.map[newcellToStandOn].transform.position) / _speed > clock.maxTimeForStep)
            speed = Vector2.Distance(transform.position, map.map[newcellToStandOn].transform.position) / clock.maxTimeForStep;
        else
            speed = _speed;

        pos.SetMapPosition(newcellToStandOn);

        moving = true;
        oldCellToStandOn = newcellToStandOn;
    }
    public void SetNextCell(Vector2Int v)
    {
        if (map.map.ContainsKey(v) && !map.map[v].isBlocked)
            newcellToStandOn = v;
    }
}
