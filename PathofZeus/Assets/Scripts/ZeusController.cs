using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusController : MonoBehaviour
{
    private SpriteRenderer renderer;

    public Transform enemie;

    public GameObject lightBolder;

    public Transform lightBoldsSpawnPlace;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(SpawnLightBolds());
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(enemie.position.x,transform.position.y,transform.position.z), 5 * Time.deltaTime);
        if (transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private IEnumerator SpawnLightBolds() 
    {
        while (true)
        {
            Instantiate(lightBolder, lightBoldsSpawnPlace.position, lightBoldsSpawnPlace.rotation);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
