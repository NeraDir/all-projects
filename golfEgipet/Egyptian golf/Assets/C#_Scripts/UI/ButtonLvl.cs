using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonLvl : MonoBehaviour
{
    public int num = 0;
    public void OnClick()
    {
        MainMenu.instance.StartLvl(num);
    }
}
