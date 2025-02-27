using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LIVEPLAYERSCOMPONENT : MonoBehaviour
{
    [SerializeField] private Transform _finish; 
    [SerializeField] private GameObject _playerTextPrefab; 
    [SerializeField] private Transform _leaderboardPanel; 

    private Dictionary<Transform, Text> _playerTexts = new Dictionary<Transform, Text>();

    private void Start()
    {
        StartCoroutine(UpdateLeaderboard());
    }

    public void RegisterPlayer(Transform player)
    {
        if (!_playerTexts.ContainsKey(player))
        {
            GameObject newTextObj = Instantiate(_playerTextPrefab, _leaderboardPanel);
            Text textComponent = newTextObj.GetComponent<Text>();

            _playerTexts.Add(player, textComponent);
        }
    }

    private IEnumerator UpdateLeaderboard()
    {
        while (true)
        {
            UpdatePlayerPositions();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void UpdatePlayerPositions()
    {
        List<Transform> sortedPlayers = new List<Transform>(PLAYERSSETUPERCOMPONENT.playersList);
        sortedPlayers.Sort((a, b) =>
            Vector3.Distance(a.position, _finish.position).CompareTo(Vector3.Distance(b.position, _finish.position))
        );

        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            Transform player = sortedPlayers[i];
            if (_playerTexts.TryGetValue(player, out Text playerText))
            {
                MOVEMENTCOMPONENT movement = player.GetComponent<MOVEMENTCOMPONENT>();
                playerText.text = $"{i + 1}. {movement.myName}";

                playerText.transform.SetSiblingIndex(i);
            }
        }
    }
}
