using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int currentWinValue = 0;

    private float m_SickEnemiesHealth = 2;

    private float m_BossEnemiesHealth = 6;

    [SerializeField]
    private TMP_Text m_WaveDisplay;

    [SerializeField]
    private TMP_Text m_PointsDisplay;

    [SerializeField]
    private GameObject m_PreWaveTxt;

    [SerializeField]
    private EnemieHealth m_SickEnemies;

    [SerializeField]
    private EnemieHealth m_StrenghtEnemies;

    [SerializeField]
    private Transform[] m_SpawnPositions;

    private int m_WaveIndex = 0;

    private float m_WaveWaitingTime = 5;

    private int m_CountOfSickEnemies = 2;

    private float m_enemiesDamage = 1;

    private int m_CountOfStrenghtEnemies = 1;

    private void Awake() => StartCoroutine(WaveControlling());

    public static int currentDestroyedCount = 0;

    private bool m_WaveStarted;

    private int m_SpawnedSickEnemies;

    private int m_SpawnedStrenghtEnemies;

    private void LateUpdate()
    {
        m_PointsDisplay.text = "x" + currentWinValue.ToString();
        m_WaveDisplay.text = m_WaveIndex.ToString();
    }


    private IEnumerator WaveControlling()
    {
        while (true)
        {
            if (currentDestroyedCount >= m_CountOfSickEnemies + m_CountOfStrenghtEnemies)
            {
                m_WaveStarted = false;
            }

            if (!m_WaveStarted)
            {
                m_SpawnedStrenghtEnemies = 0;
                m_SpawnedSickEnemies = 0;
                m_PreWaveTxt.SetActive(true);
                m_WaveIndex++;
                currentDestroyedCount = 0;
                if (m_WaveIndex != 0)
                {
                    m_SickEnemiesHealth++;
                    m_BossEnemiesHealth++;
                    m_CountOfSickEnemies++;
                }
                if (m_WaveIndex % 5 == 0)
                    m_CountOfStrenghtEnemies++;
                yield return new WaitForSeconds(m_WaveWaitingTime);
                m_WaveStarted = true;
            }
            else
            {
                if (m_SpawnedStrenghtEnemies < m_CountOfStrenghtEnemies && m_SpawnedSickEnemies < m_CountOfSickEnemies)
                {
                    m_PreWaveTxt.SetActive(false);
                    bool tempBool = SpawnSickEnemies();
                    yield return new WaitForSeconds(1);
                    if (tempBool)
                        SpawnStrenghtEnemies();

                }
                yield return null;
            }
        }
    }


    private bool SpawnSickEnemies()
    {
        for (int i = 0; i < m_CountOfSickEnemies; i++)
        {
            EnemieHealth enemie = Instantiate(m_SickEnemies, m_SpawnPositions[Random.Range(0, m_SpawnPositions.Length)].position, m_SpawnPositions[Random.Range(0, m_SpawnPositions.Length)].rotation);
            enemie.Init(m_SickEnemiesHealth);
            enemie.GetComponent<EnemieAttack>().Damage = m_enemiesDamage * 2;
            m_SpawnedSickEnemies++;
            
        }
        return true;
    }

    private void SpawnStrenghtEnemies()
    {
        for (int i = 0; i < m_CountOfStrenghtEnemies; i++)
        {
            EnemieHealth enemie = Instantiate(m_StrenghtEnemies, m_SpawnPositions[Random.Range(0, m_SpawnPositions.Length)].position, m_SpawnPositions[Random.Range(0, m_SpawnPositions.Length)].rotation);
            enemie.Init(m_BossEnemiesHealth);
            enemie.GetComponent<EnemieAttack>().Damage = m_enemiesDamage * 2;
            m_SpawnedStrenghtEnemies++;
            
        }
    }
}
