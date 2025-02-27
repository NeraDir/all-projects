using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GRIDCOMPONENT : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private float space;

    private void LateUpdate()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            if (i == 0)
            {
                transform.GetChild(i).DOScale(Vector3.one, 0.25f);
            }
            else
            {
                transform.GetChild(i).DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.25f);
            }
        }
    }
}
