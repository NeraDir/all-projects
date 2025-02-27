using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _lockPanel;

    private int _index;
    private Vector3 _beginScale;

    public void Init(int index)
    {
        _beginScale = transform.localScale;
        _index = index;
        UpdateItemVisual();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_lockPanel.activeInHierarchy)
            return;

        transform.DOScale(_beginScale / 1.5f, 0.12f).OnComplete(() =>
        {
            transform.DOScale(_beginScale, 0.12f).OnComplete(() =>
            {
                TigerClawsGameData.TigerClawsMCurentLevel = _index;
                SceneManager.LoadScene("Game");
            });
        });
    }

    private void UpdateItemVisual()
    {
        _lockPanel.SetActive(!(_index <= TigerClawsGameData.TigerClawsMaxReachedLevels));
        _text.text = (_index + 1).ToString();
    }

    
}
