using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCrazBetweenComponent : MonoBehaviour
{
    public Action action;

    public void OnBetweenDoAction()
    {
        action?.Invoke();
        action = null;
    }

    public void OnBetweenEnd()
    {
        MagicCrazButtomComponent.isPressed = false;
        gameObject.SetActive(false);
    }
}
