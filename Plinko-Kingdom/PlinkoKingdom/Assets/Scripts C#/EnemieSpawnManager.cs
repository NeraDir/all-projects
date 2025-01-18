using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_SimpleEnemies;

    [SerializeField]
    private GameObject m_BossEnemie;

    [SerializeField]
    private Transform[] m_SpawnPositions;

}
