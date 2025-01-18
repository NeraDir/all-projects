using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class miniGameTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _resultPanel;

    public static bool triggered;

    [SerializeField]
    private TMP_Text _my_count;

    [SerializeField]
    private TMP_Text _showResulte;


    [SerializeField]
    private TMP_Text _inGameResult;

    [SerializeField]
    private TMP_Text _showEndResult;

    private float _mycount;
    public static float tempValue = 0;
    public static int savedValue 
    {
        get 
        {
            if (PlayerPrefs.HasKey("savedValueSaveKey"))
            {
                return PlayerPrefs.GetInt("savedValueSaveKey");
            }
            return 0;   
        }
        set 
        {
            PlayerPrefs.SetInt("savedValueSaveKey", value);
        }
    }

    private void Awake()
    {
        tempValue = 0;
        triggered = false;
        _mycount = Random.Range(0, 10);
        _my_count.text = "x" + _mycount.ToString("0");
    }

    private void OnDestroy()
    {
        savedValue = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ballMiniGame ball))
        {
            if (triggered)
                return;
            triggered = true;
            _inGameResult.text = savedValue.ToString();
            _showResulte.text = _mycount.ToString("0");
            tempValue = _mycount * savedValue;
            _showEndResult.text = tempValue.ToString();
            _resultPanel.SetActive(true);
        }
    }

    public void Menu() 
    {
        PrefsControl.ChageGoald((int)tempValue);
        SceneManager.LoadScene("Golf_Menu");
    }
}
