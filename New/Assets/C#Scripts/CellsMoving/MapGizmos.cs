using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR_WIN

public class MapGizmos : MonoBehaviour
{
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private GUIStyle _TextStyle;
    private BoundsInt bounds;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Vector4(1, 1, 1, 1f);
        bounds = _tileMap.cellBounds;

        for (int y = bounds.min.y; y < bounds.max.y; y++)
        {
            for (int x = bounds.min.x; x < bounds.max.x; x++)
            {
                if (!_tileMap.HasTile(new Vector3Int(x, y, 0)))
                    continue;
                var cellWorldPosition = _tileMap.GetCellCenterWorld(new Vector3Int(x, y, 0));

                Handles.Label(new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z), x + ";" + y, _TextStyle);
            }
        }
        Gizmos.color = Color.white;
    }
}
#endif