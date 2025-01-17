using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CerclerComponentersf : MonoBehaviour
{
    public bool isEndRotate;

    public Text showX;

    public CirclesTrigger trigger;

    private Animator animator;

    [SerializeField]
    private GameObject _cerclerScreen;

    public static UnityEvent cerclerEnd = new UnityEvent();

    private void Start()
    {
        isEndRotate = false;
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (isEndRotate)
            showX.text = "x" + trigger.currentIndex.ToString();
    }

    public void GetResult() 
    {
        GameController.xValue = trigger.currentIndex;
        Invoke(nameof(OnCloseScreen),2);
    }

    private void OnCloseScreen() 
    {
        _cerclerScreen.SetActive(false);
        cerclerEnd?.Invoke();
    }

    public void OnClickStartRotate() 
    {
        if (isEndRotate)
            return;
        isEndRotate = true;
        animator.enabled = true;
    }
}
