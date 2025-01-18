using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _cellsContainer;
    [SerializeField] private int _cellsSortingOrder;
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private Color cellsColor;

    private BoundsInt bounds;
    public Dictionary<Vector2Int, MapCell> map;

    public static Map instance;
    private void Awake()
    {
        if (instance != null)
            Debug.LogError("more than one such class on the Scene");
        instance = this;
        AwakeMap();
    }
    List<Vector2Int> oldPath;
    private void AwakeMap()
    {
        oldPath = new List<Vector2Int>();
        map = new Dictionary<Vector2Int, MapCell>();
        bounds = _tileMap.cellBounds;

        for (int y = bounds.min.y; y < bounds.max.y; y++)
        {
            for (int x = bounds.min.x; x < bounds.max.x; x++)
            {
                if (!_tileMap.HasTile(new Vector3Int(x, y, 0)))
                    continue;
                if (map.ContainsKey(new Vector2Int(x, y)))
                    continue;
                GameObject cell = Instantiate(_cellPrefab, _cellsContainer);
                var cellWorldPosition = _tileMap.GetCellCenterWorld(new Vector3Int(x, y, (int)_cellsContainer.transform.position.z));

                cell.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z);
                cell.GetComponent<MapCell>().Render(_cellsSortingOrder, new Color(1, 1, 1, 0));
                cell.GetComponent<MapCell>().gridLocation = new Vector2Int(x, y);

                map.Add(new Vector2Int(x, y), cell.gameObject.GetComponent<MapCell>());
            }
        }
    }

    public void RenderPath(List<Vector2Int> newPath)
    {
        if (newPath == null && oldPath != null)
        {
            if (oldPath != null)
                foreach (var cell in oldPath)
                {
                    if (map.ContainsKey(cell))
                        map[cell].OffPathImage();
                }
            return;
        }
        if (oldPath != null)
        {
            foreach (var cell in oldPath)
            {
                map[cell].OffPathImage();
            }
        }
        for (int i = 0; i < newPath.Count; i++)
        {
            map[newPath[i]].OnPathImage();
        }
        oldPath = newPath;
    }
}
