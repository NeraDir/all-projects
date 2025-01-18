using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> models;

    public delegate void GameOver();
    public static event GameOver DetectTree;

    private bool hasTriggerEnter;

    private void OnEnable()
    {
        hasTriggerEnter = false;
        int randModelIndex = Random.Range(0, models.Count);
        for (int i = 0; i < models.Count; i++)
        {
            if (i == randModelIndex)
            {
                models[i].SetActive(true);
            }
            else
            {
                models[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MovementManager panther))
        {
            if (!hasTriggerEnter)
            {
                hasTriggerEnter = true;

                if (DetectTree != null)
                    DetectTree();

            }
        }
    }
}
