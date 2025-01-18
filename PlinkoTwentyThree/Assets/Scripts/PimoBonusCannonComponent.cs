using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimoBonusCannonComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _ballPref;

    [SerializeField]
    private Transform _ballSpawnPosition;

    [SerializeField]
    private Material[] _ballMaterials;

    private void LateUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out hit))
        {
            if (Input.GetMouseButtonDown(0) && PimoGameController._ballsCount > 0)
            {
                OnSpawnBall();
                transform.LookAt(hit.point);
            }
        }
    }

    private void OnSpawnBall()
    {
        GameObject _ball = Instantiate(_ballPref, _ballSpawnPosition.position, _ballSpawnPosition.rotation);
        _ball.GetComponent<MeshRenderer>().material = _ballMaterials[Random.Range(0, _ballMaterials.Length)];
    }
}
