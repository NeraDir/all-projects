using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingArrowerShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrow;

    private float _timer;

    private void LateUpdate()
    {
        _timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && _timer >= 1)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hit))
            {
                transform.LookAt(hit.point);
                if (_arrow != null)
                {
                    GameObject arrow = Instantiate(_arrow,_arrow.transform.position,_arrow.transform.rotation);
                    arrow.AddComponent<GamingArrowMovement>();
                    Destroy(arrow.GetComponent<GamingArrowerShooter>());
                    
                }
            }
            _timer = 0;
        }
    }
}
