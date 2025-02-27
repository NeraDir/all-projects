using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerComponent : MonoBehaviour
{
    [SerializeField] private GameObject _crystallPrefab;
    [SerializeField] private float _distance;
    private GameObject _lastCrystall;

    public bool isRight;

    public void Init()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            if (_lastCrystall != null)
            {
                if (isRight)
                {
                    if (_lastCrystall.transform.position.x < transform.position.x - _distance)
                        _lastCrystall = Instantiate(_crystallPrefab, transform.position, Quaternion.identity, transform.parent);
                }
                else
                {
                    if (_lastCrystall.transform.position.x > _distance + transform.position.x)
                        _lastCrystall = Instantiate(_crystallPrefab, transform.position, Quaternion.identity, transform.parent);
                }
            }
            else
            {
                _lastCrystall = Instantiate(_crystallPrefab, transform.position, Quaternion.identity, transform.parent);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
