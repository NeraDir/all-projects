using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevengerCamFollowe : MonoBehaviour
{
    public Transform gpdfphgf;

    public Vector3 fdpoogod;

    public float pdfpgdosfgds;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, gpdfphgf.position + fdpoogod, pdfpgdosfgds * Time.deltaTime);
    }
}
