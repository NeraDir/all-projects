using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BonusBallComponent : MonoBehaviour
{
    [SerializeField]
    private Material[] _ballMaterials;

    private Rigidbody _ballBody;

    private void Start()
    {
        GetComponent<MeshRenderer>().material = _ballMaterials[Random.Range(0,_ballMaterials.Length)];
        _ballBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BonusPlaceComponent place))
        {
            _ballBody.velocity = Vector3.zero;
            Destroy(_ballBody);
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(gameObject);BonusGameManager.endScoreValue += 5 * place.MultiPlay; });
        }
    }
}
