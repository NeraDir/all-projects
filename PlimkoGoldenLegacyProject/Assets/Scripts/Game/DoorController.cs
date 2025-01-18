using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public ColorVariant ColorVar;
    public Image DoorIMG;

    public Sprite ClosedDoorSprite;
    public Sprite OpenedDoorSprite;

    public bool OpenBool = false;

    public void Click()
    {
        if (!OpenBool)
        {
            DoorIMG.sprite = OpenedDoorSprite;
            OpenBool = true;

            StartCoroutine(OpenTimer());
        }
    }

    IEnumerator OpenTimer()
    {
        yield return new WaitForSeconds(2f);
        OpenBool = false;
        DoorIMG.sprite = ClosedDoorSprite;
    }
}

public enum ColorVariant
{
    Red,
    Blue,
    Green
}