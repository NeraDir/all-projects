using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private ColorType colorType;



    public ColorType GetColorType()
    {
        return colorType;
    }
}
