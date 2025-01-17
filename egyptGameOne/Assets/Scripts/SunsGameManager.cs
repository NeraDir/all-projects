using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SunsGameManager : MonoBehaviour
{
    public static int ReachedLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("SunsOfEgyptReachedLevelSaveKey"))
                return PlayerPrefs.GetInt("SunsOfEgyptReachedLevelSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("SunsOfEgyptReachedLevelSaveKey", value);
        }
    }

    public static int CurrentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("SunsOfEgyptCurrentLevelSaveKey"))
                return PlayerPrefs.GetInt("SunsOfEgyptCurrentLevelSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("SunsOfEgyptCurrentLevelSaveKey", value);
        }
    }

    [SerializeField]
    private TMP_Text _currentLevelTxt;

    [SerializeField]
    private SunsOfEgyptEnemieManager _enemieManager;

    [SerializeField]
    private SunsOfEgyptPlayerManager _playerManager;

    [SerializeField]
    private Transform[] _objectsToDo;

    [SerializeField]
    private GameObject _winScreen;

    [SerializeField]
    private GameObject _looseScreen;

    public static bool playerAttack;

    public static UnityEvent onEndFunction = new UnityEvent();
    public static UnityEvent<bool> onEnd = new UnityEvent<bool>();

    private void Awake()
    {
        playerAttack = false;
        _currentLevelTxt.text = "LEVEL " + (CurrentLevel+1).ToString();
        if (CurrentLevel > ReachedLevel)
        {
            ReachedLevel = CurrentLevel;
        }
        onEnd.AddListener(OnEnd);
    }

    private void OnDestroy()
    {
        onEnd.RemoveListener(OnEnd);
    }

    private void OnEnd(bool value)
    {
        if (value)
        {
            _winScreen.SetActive(true);
        }
        else
        {
            _looseScreen.SetActive(true);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        foreach (var item in _objectsToDo)
        {
            item.DOScale(Vector3.one, 0.25f);
        }
        yield return new WaitForSeconds(0.5f);
        _enemieManager.AddCard();
        _playerManager.Init();
    }

    public void OnClickNext()
    {
        CurrentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart()
    {
        CurrentLevel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        CurrentLevel = 0;
        Scene nextScene = SceneManager.CreateScene("SunsofEgyptMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }
}
