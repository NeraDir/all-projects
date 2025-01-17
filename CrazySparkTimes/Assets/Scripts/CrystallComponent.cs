using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrystallComponent : MonoBehaviour
{
    private int _crystallIndex;
    private Image _crystallImage;
    private float _crystallScale;
    private int _score;

    public int CrystallIndex => _crystallIndex;

    private GameManager _gameManager;

    [SerializeField]
    private TMP_Text _scoreDispalyObject;

    private CrystallData _nextData;

    public bool isTriggered;

    public void Init(CrystallData data) 
    {
        isTriggered = false;
        _gameManager = FindObjectOfType<GameManager>();
        _crystallImage = GetComponent<Image>();
        int mineIndex = _gameManager.CrystallConfig.CrystallsDatas.IndexOf(data);
        if (mineIndex < _gameManager.CrystallConfig.CrystallsDatas.Count - 1)
        {
            _nextData = _gameManager.CrystallConfig.CrystallsDatas[mineIndex + 1];
        }
        _score = data.Score;
        transform.localScale = Vector3.zero;
        _crystallIndex = data.Index;
        _crystallImage.sprite = data.Sprite;
        _crystallScale = data.Scale;
        StartCoroutine(Moving());
    }

    private IEnumerator Moving() 
    {
        while (transform.localScale.x != _crystallScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(_crystallScale, _crystallScale, _crystallScale), 10 * Time.deltaTime);
            yield return null;
        }
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CrystallComponent crystall))
        {
            if (crystall.CrystallIndex == CrystallIndex && !isTriggered)
            {
                crystall.isTriggered = true;
                isTriggered = true;
                Destroy(other.gameObject);
                if (_nextData != null)
                {
                    CrystallComponent tempCrystall = Instantiate(this, transform.position, transform.rotation,transform.parent);
                    tempCrystall.Init(_nextData);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.currentScore += _score;
        TMP_Text text = Instantiate(_scoreDispalyObject, transform.position, Quaternion.identity, transform.parent);
        text.text = "+" + _score;
    }
}
