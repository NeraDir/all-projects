using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChaseLevelCiontainerComponent : MonoBehaviour
{
    [SerializeField] private Text _showLevelText;
    [SerializeField] private GameObject _levelLockPanel;

    private Button _levelButton;

    private int _level;

    public int level
    {
        private get => _level;
        set => _level = value;
    }

    private void Awake()
    {
        _levelLockPanel.SetActive(_level > ChasePlayerDataComponent.ChasePlayerMaxReachedLevel);
        _showLevelText.text = (_level + 1).ToString();
        
        _levelButton = GetComponent<Button>();
        _levelButton.onClick.AddListener(() =>
        {
            OnLevelButtonPressed();
        });
    }

    private void OnLevelButtonPressed()
    {
        if(_levelLockPanel.activeInHierarchy)
            return;
        ChasePlayerDataComponent.ChasePlayerCurrentLevel = _level;
        SceneManager.LoadScene("Game");
    }
}
