using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelContainer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _lockPanel;
    [SerializeField] private Text _text;
    private int _level;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_lockPanel.activeInHierarchy)
            return;
        TlineGameDataSaves.TlineCurrentLevel = _level;
        SceneManager.LoadScene("GameScene");
    }

    public void SetupData(int level)
    {
        _level = level;
        if (_level <= TlineGameDataSaves.TlineMaxReachedLevel)
            _lockPanel.SetActive(false);
        _text.text = (_level + 1).ToString();
    }
}
