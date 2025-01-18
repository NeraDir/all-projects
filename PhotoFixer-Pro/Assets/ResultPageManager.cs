using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultPageManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text resultTXT;

    private int currentResult;
    private float resultValueLerp;

    private Animator m_animator;

    [SerializeField]
    private GameObject homeButton;

    private void OnEnable()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetInteger("state", 0);
        currentResult = GameManager.Instance.PercentResult;
        resultValueLerp = 0;
        StartCoroutine(lerpResult());
        homeButton.SetActive(false);
    }

    private void Update()
    {
        resultTXT.text = resultValueLerp.ToString("#.##") + "%";
    }

    private void OnDisable()
    {
        m_animator.SetInteger("state", 0);
        homeButton.SetActive(true);

    }

    private IEnumerator lerpResult()
    {
        while (resultValueLerp != currentResult)
        {
            resultValueLerp = Mathf.Lerp(resultValueLerp, currentResult, 0.2f);
            yield return null;
        }
    }

    public void Continue()
    {
        m_animator.SetInteger("state", 2);
    }

    public void SetIdleAnimation()
    {
        m_animator.SetInteger("state", 1);
    }

    public void RespawnGammaPerfab()
    {
        GameManager.Instance.Spawn();
        
    }
}
