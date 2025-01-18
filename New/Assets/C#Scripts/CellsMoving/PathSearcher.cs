using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathSearcher : MonoBehaviour
{
    public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal, Dictionary<Vector2Int, MapCell> field, int maxSteps)
    {
        var closedSet = new List<PathNode>();
        var openSet = new List<PathNode>();

        PathNode startNode = new PathNode()
        {
            Position = start,
            CameFrom = null,
            PathLengthFromStart = 0,
            HeuristicEstimate = GetHeuristicPathLength(start, goal)
        };

        openSet.Add(startNode);

        int iter = maxSteps;
        while (openSet.Count > 0)
        {
            if (iter <= 0)
                return null;

            var currentNode = openSet.OrderBy(node => node.EstimateFull).First();

            if (currentNode.Position == goal)
            {
                List<Vector2Int> result = GetPathForNode(currentNode);
                if (field[goal].isBlocked)
                {
                    //result.Remove(result.Last());
                }
                return result;
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            List<PathNode> list = GetNeighbours(currentNode, goal, field);
            for (int i = 0; i < list.Count; i++)
            {
                if (openSet.Contains(list[i]))
                {
                    var openNode = openSet.FirstOrDefault(node => node.Position == list[i].Position);
                    if (openNode.EstimateFull > list[i].EstimateFull)
                    {
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = list[i].PathLengthFromStart;
                    }
                }
                else
                {
                    openSet.Add(list[i]);
                    if (list[i].Position == goal)
                    {
                        List<Vector2Int> result = GetPathForNode(list[i]);
                        if (field[goal].isBlocked)
                        {
                            //result.Remove(result.Last());
                        }
                        return result;
                    }
                }
            }
            iter -= 1;
        }
        return null;
    }
    private static float GetHeuristicPathLength(Vector2Int from, Vector2Int to)
    {
        return Vector2Int.Distance(from, to);
    }
    private static List<PathNode> GetNeighbours(PathNode pathNode, Vector2Int goal, Dictionary<Vector2Int, MapCell> field)
    {
        var result = new List<PathNode>();

        Vector2Int[] neighbourPoints = new Vector2Int[8];
        neighbourPoints[0] = new Vector2Int(pathNode.Position.x + 1, pathNode.Position.y);
        neighbourPoints[1] = new Vector2Int(pathNode.Position.x - 1, pathNode.Position.y);
        neighbourPoints[2] = new Vector2Int(pathNode.Position.x, pathNode.Position.y + 1);
        neighbourPoints[3] = new Vector2Int(pathNode.Position.x, pathNode.Position.y - 1);

        neighbourPoints[4] = new Vector2Int(pathNode.Position.x + 1, pathNode.Position.y + 1);
        neighbourPoints[5] = new Vector2Int(pathNode.Position.x - 1, pathNode.Position.y - 1);
        neighbourPoints[6] = new Vector2Int(pathNode.Position.x + 1, pathNode.Position.y - 1);
        neighbourPoints[7] = new Vector2Int(pathNode.Position.x - 1, pathNode.Position.y + 1);
        int i = 0;
        for (; i < 4; i++)
        {

            if (!field.ContainsKey(neighbourPoints[i]) || (field[neighbourPoints[i]].isBlocked && neighbourPoints[i] != goal))
                continue;
            var neighbourNode = new PathNode()
            {
                Position = neighbourPoints[i],
                CameFrom = pathNode,
                PathLengthFromStart = pathNode.PathLengthFromStart + 1,
                HeuristicEstimate = GetHeuristicPathLength(neighbourPoints[i], goal)
            };
            result.Add(neighbourNode);
        }
        for (; i < 8; i++)
        {
            if (!field.ContainsKey(neighbourPoints[i]) || (field[neighbourPoints[i]].isBlocked && neighbourPoints[i] != goal))
                continue;
            var neighbourNode0 = new PathNode()
            {
                Position = neighbourPoints[i],
                CameFrom = pathNode,
                PathLengthFromStart = pathNode.PathLengthFromStart + 1.4f,
                HeuristicEstimate = GetHeuristicPathLength(neighbourPoints[i], goal)
            };
            result.Add(neighbourNode0);
        }
        return result;
    }
    private static List<Vector2Int> GetPathForNode(PathNode pathNode)
    {
        var result = new List<Vector2Int>();
        var currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.Position);
            currentNode = currentNode.CameFrom;
        }
        result.Reverse();
        return result;
    }
}

public class PathNode
{
    public Vector2Int Position { get; set; }
    public float PathLengthFromStart { get; set; }
    public PathNode CameFrom { get; set; }
    public float HeuristicEstimate { get; set; }
    public float EstimateFull
    {
        get
        {
            return this.PathLengthFromStart + this.HeuristicEstimate;
        }
    }
}