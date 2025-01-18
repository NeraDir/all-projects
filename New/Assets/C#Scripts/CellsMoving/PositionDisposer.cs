using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionDisposer : MonoBehaviour
{
    public static Dictionary<Vector2Int, PositionDisposer> allPositions;
    [SerializeField] private Vector2Int mapPosition = Vector2Int.zero;
    Map map;
    Vector2Int[] Resolution = new Vector2Int[8];
    private void Awake()
    {
        allPositions = new Dictionary<Vector2Int, PositionDisposer>();
    }
    private void Start()
    {
        Resolution[0] = new Vector2Int(1, 0);
        Resolution[1] = new Vector2Int(-1, 0);
        Resolution[2] = new Vector2Int(0, 1);
        Resolution[3] = new Vector2Int(0, -1);

        Resolution[4] = new Vector2Int(1, 1);
        Resolution[5] = new Vector2Int(-1, -1);
        Resolution[6] = new Vector2Int(1, -1);
        Resolution[7] = new Vector2Int(-1, 1);

        map = Map.instance;
        SetMapPosition(mapPosition);
        AttachMapPosition();
    }
    public void DestroyThis()
    {
        allPositions.Remove(mapPosition);

        map.map[mapPosition].isBlocked = false;
        Destroy(gameObject);
    }
    public bool SetMapPosition(Vector2Int position)
    {
        if (map.map.ContainsKey(position) && !map.map[position].isBlocked)
        {
            allPositions.Remove(mapPosition);
            allPositions.Add(position, this);

            map.map[mapPosition].isBlocked = false;
            mapPosition = position;
            map.map[position].isBlocked = true;
            return true;
        }
        Debug.LogWarning("try SetMapPosition on bloked Cell");
        return false;
    }
    public Vector2Int GetMapPosition()
    {
        return mapPosition;
    }
    public void AttachMapPosition()
    {
        transform.position = map.map[mapPosition].transform.position;
    }
    public List<Vector2Int> GetAroundCells()
    {
        List<Vector2Int> res = new List<Vector2Int>();
        for (int i = 0; i < Resolution.Length; i++) {

            if(map.map.ContainsKey(Resolution[i] + mapPosition) && map.map[Resolution[i] + mapPosition].isBlocked)
            {
                res.Add(Resolution[i] + mapPosition);
            }
        }
        return res;
    }
}
