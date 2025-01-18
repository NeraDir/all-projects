using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField]
    private ObstacleType type;

    public virtual ObstacleType GetObstacleType()
    {
        return type;
    }
}

public enum ObstacleType
{
    Default,
    Banan,
    Gelll
}
