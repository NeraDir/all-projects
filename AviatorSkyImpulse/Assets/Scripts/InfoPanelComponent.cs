using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfoPanelComponent : MonoBehaviour
{
    private bool isNormal = true;
    private IEnumerator Start()
    {
        isNormal = true;
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            if (Random.Range(0, 2) != 0)
            {
                transform.DORotate(new Vector3(0,0,transform.rotation.z + Random.Range(0,2) != 0? -10:10), 1, RotateMode.Fast);
                FindObjectOfType<GameManager>().AttentionPlayer.Play();
                GameManager.isCriticalStatus = true;
                isNormal = false;
            }
        }
    }

    public void OnClickMageDefault(bool isRight)
    {
        if (isNormal)
            return;
        if(isRight)
            transform.DORotate(new Vector3(0, 0, transform.rotation.z + 10), 1, RotateMode.WorldAxisAdd);
        else
            transform.DORotate(new Vector3(0, 0, transform.rotation.z + -10), 1, RotateMode.WorldAxisAdd);

        if (transform.rotation.z < 1 && transform.rotation.z > -1)
        {
            isNormal = true;
            GameManager.isCriticalStatus = false;
            FindObjectOfType<GameManager>().AttentionPlayer.Stop();
        }
    }
}
