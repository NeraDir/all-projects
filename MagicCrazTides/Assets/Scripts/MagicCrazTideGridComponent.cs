using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MagicCrazTideGridComponent : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private float space;
    [SerializeField] private int maxChildCount;

    private void LateUpdate()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            int row = i / x;
            int col = i / x;

            if (i == 0)
            {
                transform.GetChild(0).localScale = new Vector3(100, 100, 100);
            }
            else
            {
                transform.GetChild(i).localScale = new Vector3(80, 80, 80);
            }
            transform.GetChild(i).localPosition = new Vector3(0, 0, -(col * space));
            
        }
    }
}
