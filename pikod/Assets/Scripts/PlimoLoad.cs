using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlimoLoad : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private GameObject _ballImage;

    [SerializeField]
    private Sprite[] _ballsSprites;

    [SerializeField]
    private float _waitingTIme;

    private float _currentTime;

    private IEnumerator Start()
    {
        _currentTime = 0;
        while (_currentTime < _waitingTIme)
        {
            yield return new WaitForSeconds(0.25f);
            GameObject tempball = Instantiate(_ballImage, new Vector2(Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x), _spawnPositions[0].position.y + 10), Quaternion.Euler(0, 0, Random.Range(-360, 360)), _spawnPositions[0].transform.parent.transform);
            Image tempImage = tempball.GetComponent<Image>();
            tempImage.sprite = _ballsSprites[Random.Range(0,_ballsSprites.Length)];
            _currentTime += 0.25f;
        }
        SceneManager.LoadScene("PlimoMenu");
    }
}
