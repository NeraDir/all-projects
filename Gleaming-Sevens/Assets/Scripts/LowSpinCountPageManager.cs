using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowSpinCountPageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject winPanel;

    [SerializeField]
    private SlotPageManager slotPageManager; 

    

    private void OnEnable()
    {
        winPanel.SetActive(false);
        StartCoroutine(lifeTimePage());
    }

    private IEnumerator lifeTimePage()
    {
        yield return new WaitForSeconds(3.0f);
        slotPageManager.ClosePage();
        gameObject.SetActive(false);
    }


}
