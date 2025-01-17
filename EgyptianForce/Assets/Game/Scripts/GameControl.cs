using Game.Shop;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private Text _endColleted;
    [SerializeField] private Text _endAllHave;

    [SerializeField] private GameObject _player;
    [SerializeField] private Transform[] _playerSpawnPoints;

    [SerializeField] private Money _money;
    [SerializeField] private EnemySpawn _enemySpawn;

    [SerializeField] private Text _bagsCollectedText;
    public int bagsCollected;
    public int bagsNeed;

    private void Start() => _enemySpawn.SpawnEnemies();

    public void CollectBag()
    {
        bagsCollected++;
        if(bagsCollected >= bagsNeed) OpenEndPanel();
        UpdateBagsDisplay();
    }

    public string UpdateBagsDisplay()
    {
        string collectedText = _bagsCollectedText.text = $"{bagsCollected}/{bagsNeed}";
        return collectedText;
    }


    private void OpenEndPanel()
    {
        _endPanel.SetActive(true);
        _money.AddMoney(bagsCollected);

        _endColleted.text = UpdateBagsDisplay();
        _endAllHave.text = _money.money.ToString();
    }

    public void RestartGame()
    {
        _endPanel.SetActive(false);
        bagsCollected = 0;
        bagsNeed = 0;

        _enemySpawn.SpawnEnemies();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _player.transform.position = _playerSpawnPoints[Random.Range(0, _playerSpawnPoints.Length)].position;
    }
}