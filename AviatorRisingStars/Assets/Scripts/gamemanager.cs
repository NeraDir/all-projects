using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public static int gametestcanvastopmarginsvalue
    {
        get
        {
            if (PlayerPrefs.HasKey("gametestcanvastopmarginsvaluesave"))
            {
                return PlayerPrefs.GetInt("gametestcanvastopmarginsvaluesave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("gametestcanvastopmarginsvaluesave", value);
        }
    }

    public static string gametestsettingkey;

    public static int gametestcanvastoolbarshowstate
    {
        get
        {
            if (PlayerPrefs.HasKey("gametestcanvastoolbarshowstatesave"))
            {
                return PlayerPrefs.GetInt("gametestcanvastoolbarshowstatesave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("gametestcanvastoolbarshowstatesave", value);
        }
    }

    public static int gamestarsconscount
    {
        get
        {
            if (PlayerPrefs.HasKey("gamestarsconscountsave"))
            {
                return PlayerPrefs.GetInt("gamestarsconscountsave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("gamestarsconscountsave", value);
        }
    }

    public static float maxdistancereachedvalue
    {
        get
        {
            if (PlayerPrefs.HasKey("maxdistancereachedvaluesave"))
            {
                return PlayerPrefs.GetFloat("maxdistancereachedvaluesave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("maxdistancereachedvaluesave", value);
        }
    }

    public static bool gameRunned;
    public static int currentCoins;

    [SerializeField]
    private Transform _playerTransform;

    private Vector3 _playerStartPosition;

    [SerializeField]
    private GameObject _resultScreeen;

    [SerializeField]
    private Text[] _distanceTxt;

    [SerializeField]
    private Text[] _coinsTxt;

    private void Awake()
    {
        gameRunned = true;
        currentCoins = 0;
        _playerStartPosition = _playerTransform.position;
        playercontroller.PlayerIsDeath.AddListener(OnPlayerDeath);
    }

    private void LateUpdate()
    {
        if (!gameRunned)
            return;
        float currentDistance = Vector3.Distance(_playerStartPosition, _playerTransform.position);
        foreach (var item in _distanceTxt)
        {
            item.text = currentDistance.ToString("0.0") + "m";
        }
        foreach (var item in _coinsTxt)
        {
            item.text = currentCoins.ToString("0") + "A";
        }
        if (currentCoins > gamestarsconscount)
        {
            gamestarsconscount = currentCoins;
        }
        if (currentDistance > maxdistancereachedvalue)
        {
            maxdistancereachedvalue = currentDistance;
        }
    }

    private void OnDestroy()
    {
        playercontroller.PlayerIsDeath.RemoveListener(OnPlayerDeath);
    }

    private void OnPlayerDeath() 
    {
        gameRunned = false;
        _resultScreeen.SetActive(true);
    }

    public void OnClickRestart() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickMenu() 
    {
        SceneManager.LoadScene("Menu");
    }
}
