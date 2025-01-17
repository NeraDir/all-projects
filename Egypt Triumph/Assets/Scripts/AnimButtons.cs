using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimButtons : MonoBehaviour
{
    public void Down()
    {
        transform.localScale = new Vector2(1.15f, 1.15f);
    }

    public void Up()
    {
        transform.localScale = new Vector2(1, 1);
    }
}
