using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiramidController : MonoBehaviour
{
    [SerializeField] private List<Transform> pos = new List<Transform>();
    [SerializeField] private GameObject prefabPiramid;

    List<PiramidAnimator> piramids = new List<PiramidAnimator>();
    public List<PiramidAnimator> SetPiramid(int id)
    {
        piramids.Clear();

        for (int i = 0; i < id; i++)
        {
            GameObject inst = Instantiate(prefabPiramid, pos[i].position, pos[i].rotation, pos[i]);
            piramids.Add(inst.GetComponent<PiramidAnimator>());
        }

        return piramids; // piramids
    }

}
