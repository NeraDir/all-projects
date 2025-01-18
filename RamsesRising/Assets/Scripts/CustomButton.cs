using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomButton : MonoBehaviour
{
    public bool isActive;

    public Animator animator;
    public GameObject objecter;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ClickButton);
    }

    private void ClickButton() 
    {
        if (isActive)
            return;
        StartCoroutine(ClickedIEN());
    }

    private IEnumerator ClickedIEN() 
    {
        isActive = true;
        animator.SetBool("RAMANIMA", true);
        yield return new WaitForSeconds(0.5f);
        objecter.SetActive(true);
        animator.gameObject.SetActive(false);
        isActive = false;
    }
}
