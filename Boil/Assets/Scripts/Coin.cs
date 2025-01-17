using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public GameObject destroyParticelEfffect;


    private void OnDestroy()
    {
        Instantiate(destroyParticelEfffect, transform.position, Quaternion.identity);
        //Debug.Log("Par  CALL!");
    }

}
