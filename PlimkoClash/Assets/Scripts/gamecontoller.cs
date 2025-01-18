using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class BgImages
{
    [SerializeField]
    private Sprite[] _bgsprites;

    public Sprite GetSprite() 
    {
        return _bgsprites[Random.Range(0, _bgsprites.Length)];
    }
}

public class gamecontoller : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _grounds;

    [SerializeField]
    private Transform _checkPoint;

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private GameObject _resultScreen;

    [SerializeField]
    private Text[] _starsCountTxt;

    [SerializeField]
    private Text[] _distanceTxt;

    [SerializeField]
    private Text[] _ballsCountTxt;

    [SerializeField]
    private Image _bgImage;

    [SerializeField]
    private BgImages[] _levelSprites;

    private GameObject _lastGround;

    public static float groundMoveSpeed;

    public static int starsCount;

    public static float currentDistance;

    public static int ballsCount;

    public static int levelIndex;

    public static float maxDistance 
    {
        get
        {
            if (PlayerPrefs.HasKey("BallsMaxDistanceSaveKey"))
            {
                return PlayerPrefs.GetFloat("BallsMaxDistanceSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("BallsMaxDistanceSaveKey", value);
        }
    }

    public static int ballStars
    {
        get
        {
            if (PlayerPrefs.HasKey("BallsStarsCountSaveKey"))
            {
                return PlayerPrefs.GetInt("BallsStarsCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BallsStarsCountSaveKey", value);
        }
    }

    private IEnumerator Start()
    {
        groundMoveSpeed = 2;
        starsCount = 0;
        currentDistance = 0;
        ballsCount = 0;
        _bgImage.sprite = _levelSprites[levelIndex].GetSprite();
        ballmovement.ballIsDestroyed.AddListener(OnBallDestroyed);
        while (true)
        {

            if (_lastGround != null)
            {
                if (_lastGround.transform.position.x <= _checkPoint.position.x)
                {
                    _lastGround = Instantiate(_grounds[Random.Range(0, _grounds.Length)], _spawnPoint.position, Quaternion.identity, _checkPoint.parent);
                    _lastGround.transform.SetSiblingIndex(0);
                }
            }
            else
            {
                _lastGround = Instantiate(_grounds[Random.Range(0, _grounds.Length)], _spawnPoint.position, Quaternion.identity, _checkPoint.parent);
                _lastGround.transform.SetSiblingIndex(0);
            }
            yield return null;
        }
    }

    private void LateUpdate()
    {
        groundMoveSpeed += 0.005f * levelIndex + 1;
        currentDistance += 0.1f;
        if (currentDistance > maxDistance)
        {
            maxDistance = currentDistance;
        }
        foreach (var item in _ballsCountTxt)
        {
            item.text = ballsCount.ToString("0") + "b";
        }
        foreach (var item in _distanceTxt)
        {
            item.text = currentDistance.ToString("0.0") + "m";
        }
        foreach (var item in _starsCountTxt)
        {
            item.text = starsCount.ToString("0") + "c";
        }
    }

    private void OnDestroy()
    {
        ballmovement.ballIsDestroyed.RemoveListener(OnBallDestroyed);
    }

    private void OnBallDestroyed() 
    {
        SceneManager.LoadScene("bonusgame");
    }
}
