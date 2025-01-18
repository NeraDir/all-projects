using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class other : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        GameObject gm1 = GameObject.Find("Image up");
        foreach (Transform tr in gm1.transform)
        {
            tr.gameObject.SetActive(false);
        }
        gm1 = GameObject.Find("Image buy");
        foreach (Transform tr in gm1.transform)
        {
            tr.gameObject.SetActive(false);
        }
    }
    void OnMouseDown()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
