using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int wondeHelpedPeoplesRecordCount 
    {
        get 
        {
            if (PlayerPrefs.HasKey("wondeHelpedPeoplesRecordCountSave"))
                return PlayerPrefs.GetInt("wondeHelpedPeoplesRecordCountSave");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("wondeHelpedPeoplesRecordCountSave", value);
        }
    }

    public static int wonderScreenScale
    {
        get
        {
            if (PlayerPrefs.HasKey("wonderScreenScaleSave"))
            {
                return PlayerPrefs.GetInt("wonderScreenScaleSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("wonderScreenScaleSave", value);
        }
    }

    public static string wonderTesterToDoConfig;

    public static float wonderPlaneHealth;

    public static int wonderHelpedPeoplesCount;

    public static int wondeBeginPeoplesForHelp
    {
        get
        {
            if (PlayerPrefs.HasKey("wondeBeginPeoplesForHelpSave"))
            {
                return PlayerPrefs.GetInt("wondeBeginPeoplesForHelpSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("wondeBeginPeoplesForHelpSave", value);
        }
    }

    public static WonderPlaneController wonderPlaneControllerComponent;

    [SerializeField]
    private GameObject[] _spawningObjects;

    [SerializeField]
    private Transform[] _spawningPoints;

    [SerializeField]
    private Text[] _showPeoplesSavedCount;

    [SerializeField]
    private Text _showHealthCount;

    [SerializeField]
    private GameObject _resultPageObject;

    [SerializeField]
    private Image _healthBar;

    [SerializeField]
    private WonderPlaneController _wonderPlaneControllerComponent;

    private IEnumerator Start()
    {
        wonderPlaneControllerComponent = _wonderPlaneControllerComponent;
        wonderHelpedPeoplesCount = 0;
        wonderPlaneHealth = 100;
        while (true)
        {
            int rndSpawnObjectIndex = Random.Range(0, _spawningObjects.Length);
            Instantiate(_spawningObjects[rndSpawnObjectIndex], new Vector3(Random.Range(_spawningPoints[0].position.x, _spawningPoints[1].position.x), _spawningObjects[rndSpawnObjectIndex].transform.position.y, _spawningPoints[0].position.z), _spawningObjects[rndSpawnObjectIndex].transform.rotation);
            yield return new WaitForSeconds(5f);
        }
    }

    private void LateUpdate()
    {
        if (wonderPlaneHealth <= 0)
        {
            _resultPageObject.SetActive(true);
            return;
        }
        _healthBar.fillAmount = Mathf.MoveTowards(_healthBar.fillAmount, (wonderPlaneHealth / 100), 10 * Time.deltaTime);
        foreach (var item in _showPeoplesSavedCount)
        {
            item.text = wonderHelpedPeoplesCount.ToString("0");
        }
        _showHealthCount.text = wonderPlaneHealth.ToString("0") + "/" + "100";
        if (wonderHelpedPeoplesCount > wondeHelpedPeoplesRecordCount)
        {
            wondeHelpedPeoplesRecordCount = wonderHelpedPeoplesCount;
        }
    }

    public void ClickMenu() 
    {

        SceneManager.LoadScene("Menu");
    }

    public void ClickRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
