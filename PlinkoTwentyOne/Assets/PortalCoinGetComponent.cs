using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortalCoinGetComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _effect;

    [SerializeField]
    private TMP_Text _xShow;

    private int _rndX;

    public bool isRing;

    private void Start()
    {
        if (!isRing)
            return;
        _rndX = Random.Range(1, 10);
        _xShow.text = "x" + _rndX.ToString();
    }

    public void OnGetUse()
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            if (isRing)
            {
                PortalSpawnRoadsComponent.currentScore *= _rndX;
            }
            else
            {
                PortalSpawnRoadsComponent.currentScore += 1;
            }
            Instantiate(_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        });
    }
}
