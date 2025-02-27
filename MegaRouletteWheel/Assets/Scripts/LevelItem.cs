using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _lock;

    private int _levels;

    public void Init(int index)
    {
        _levels = index;
        _text.text = (_levels + 1).ToString();
        if(_levels <= GameController.MaxReachedLevel)
            _lock.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_lock.activeInHierarchy)
            return;
        transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f).OnComplete(() => transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() => transform.DOScale(Vector3.one, 0.1f).OnComplete(() =>
        {
            StartCoroutine(DoSomthing());
        })));
    }

    private IEnumerator DoSomthing()
    {
        SpawnBlocks.spawn = true;
        yield return new WaitForSeconds(1.3f);
        OnOpen();
        SpawnBlocks.spawn = false;
        ButtonCustom.isClicked = false;
    }

    private void OnOpen()
    {
        GameController.Level = _levels;
        SceneManager.LoadScene("Game");
    }
}
