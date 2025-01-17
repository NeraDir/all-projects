using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelContainer : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text _levelIndexTxt;

    private int _levelIndex;

    private bool _isClicked;

    [SerializeField]
    private GameObject _closePanel;

    private void Start()
    {
        _levelIndexTxt = GetComponentInChildren<TMP_Text>();
        _closePanel= transform.GetChild(1).gameObject;
        _levelIndexTxt.text = (_levelIndex + 1).ToString();
    }

    public void SetData(int Index)
    {
        _levelIndex = Index;
        if (_levelIndex <= GameSavesData.MaxReachLevel)
            _closePanel.SetActive(false);
    }

    private void OnClickLoadLevel()
    {
        if (_closePanel.activeInHierarchy)
            return;
        if (_isClicked)
            return;
        _isClicked = true;
        SceneManager.LoadScene("CrazyGameScene");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickLoadLevel();
    }
}
