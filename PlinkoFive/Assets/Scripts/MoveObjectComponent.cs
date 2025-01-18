using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectComponent : MonoBehaviour
{
    public static float speed;

    public bool sawCanner;

    public Transform[] sawses;

    public List<GameObject> otherSaws;

    private void Start()
    {
        if (sawCanner)
        {
            if (Random.Range(0,2) != 0)
            {
                sawses[Random.Range(0, sawses.Length)].gameObject.SetActive(true);
            }
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                Destroy(otherSaws[i]);
                otherSaws.Remove(otherSaws[i]);
            }
        }
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
    }
}
