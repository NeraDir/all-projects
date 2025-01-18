using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleter : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner _enemySpawner;
    [SerializeField]
    private GameObject _losePanel;
    [SerializeField]
    private GameObject _enemyParentObject;
    [SerializeField]
    private GameObject _nextLevelDoor;
    [SerializeField]
    private Transform _startPosition;
    [SerializeField]
    private Text _killsText;
    [SerializeField]
    private Text _levelsCompleteText;
    [SerializeField]
    private Text _moneyForGameText;


    private int _kills;
    private int _levelsComplete;

    public void CheckLevelWin()
    {
        if(_enemyParentObject.transform.childCount - 1 == 0)
        {
            LevelComplete();
            _levelsComplete++;
        }
        else
        {
            _kills++;
        }
    }

    public void LevelComplete()
    {
        _nextLevelDoor.SetActive(false);
    }

    public void LevelFailed()
    {
        _levelsCompleteText.text = $"Levels completed: {_levelsComplete}";
        _killsText.text = $"Kills: {_kills}";
        _moneyForGameText.text = $"x{MoneyCounter._moneyForGame}";
        _losePanel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LoadNextLevel(other.gameObject);
        }
    }

    public void LoadNextLevel(GameObject player)
    {
        player.transform.position = _startPosition.position;
        _enemySpawner.StartEnemySpawner();
    }
}
