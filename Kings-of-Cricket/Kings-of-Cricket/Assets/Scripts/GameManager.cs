using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private testCamMovementScript _camComponente;

    [SerializeField]
    private Slider _sliderMovement;

    public static bool isGameStarted;

    [SerializeField]
    private GameObject[] _poweringPanel;

    [SerializeField]
    private TMP_Text _playerScoreDispalyer;

    [SerializeField]
    private TMP_Text _enemyScoreDispalyer;

    public static int playerScore;

    public static int enemyScore;

    private void LateUpdate()
    {
        if (!isGameStarted)
        {
            foreach (var player in _poweringPanel) 
            {
                player.SetActive(true);
            }
            _sliderMovement.value -= 0.0015f;
        }
        else
        {
            foreach (var player in _poweringPanel)
            {
                player.SetActive(false);
            }
        }

        _enemyScoreDispalyer.text = enemyScore.ToString();
        _playerScoreDispalyer.text = playerScore.ToString();
    }


    public void OnClickUpStrenght() 
    {
        if (!isGameStarted)
        {
            _sliderMovement.value += 0.1f;
        }
    }
}
