using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class platforma : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    public int x;
    public int y;
    public GameObject gm;
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
        if (gm == null)
        {
            gm1 = GameObject.Find("Image buy");
            foreach (Transform tr in gm1.transform)
            {
                if (tr.gameObject.name == "mini_pn (" + ((1 - y) * 4 + x) + ")")
                {
                    tr.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            gm1 = GameObject.Find("Image up");
            foreach (Transform tr in gm1.transform)
            {
                if (tr.gameObject.name == "mini_pn (" + ((1 - y) * 4 + x) + ")")
                {
                    tr.gameObject.SetActive(true);
                    foreach (Transform tr1 in tr)
                    {
                        if (tr1.gameObject.GetComponent<Text>() != null)
                        {
                            tr1.gameObject.GetComponent<Text>().text = (200 * GameObject.Find("func").GetComponent<func>().p_mas[x, y].GetComponent<pychka>().level).ToString();
                        }
                    }
                }
            }
        }
    }
    void OnMouseDown() {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
