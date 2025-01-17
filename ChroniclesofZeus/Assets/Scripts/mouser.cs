using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouser : MonoBehaviour
{
    [SerializeField]
    private GameObject showMouse;

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hit))
            {
                exp[] expes = FindObjectsOfType<exp>();
                foreach (var item in expes)
                {
                    if (Vector3.Distance(showMouse.transform.position,item.transform.position) < 2)
                    {
                        item.transform.position = Vector3.MoveTowards(item.transform.position,showMouse.transform.position, 4 * Time.deltaTime);
                    }
                }
                showMouse.transform.position = hit.point;
                showMouse.SetActive(true);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            showMouse.SetActive(false);
        }
    }
}
