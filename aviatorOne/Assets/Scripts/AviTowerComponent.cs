using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviTowerComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _rocketPref;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (Random.Range(0,2) != 0)
            {
                if (!AviaPlaneController.isEnd)
                {
                    GameObject tempRocket = Instantiate(_rocketPref, _rocketPref.transform.position, Quaternion.identity);
                    tempRocket.SetActive(true);
                }
            }

        }
    }
}
