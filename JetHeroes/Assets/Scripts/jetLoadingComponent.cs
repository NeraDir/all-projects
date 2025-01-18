using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class jetLoadingComponent : MonoBehaviour
{
    public TMP_Text laodingValueDisplay;

    public Transform jetCharacter;
    public ParticleSystem jetParticleSystem;

    private float loadingvalue;

    public Image loadingBar;

    private IEnumerator Start()
    {
        float yValue = jetCharacter.transform.position.y + 100;
        float yValueTem = jetCharacter.transform.position.y + 5;
        float yValueTemp = jetCharacter.transform.position.y - 5;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(jetCharacter.DOMoveY(yValueTem, 1));
        sequence.Append(jetCharacter.DOMoveY(yValueTemp, 1));
        sequence.SetLoops(-1, LoopType.Yoyo);
        jetParticleSystem.startLifetime = 0.19f;
        while (loadingvalue < 100) 
        {
            loadingvalue = Mathf.MoveTowards(loadingvalue, 101, 10 * Time.deltaTime);
            laodingValueDisplay.text = loadingvalue.ToString("0.0") + "%";
            loadingBar.fillAmount = loadingvalue / 100;
            yield return null;
        }
        sequence.Kill();
        jetCharacter.DOMoveY(yValueTemp - 10, 0.5f).OnComplete(() => { jetParticleSystem.startLifetime = 0.56f; jetCharacter.DOMoveY(yValue, 2).OnComplete(() => SceneManager.LoadScene("loadingJet"));  });
    }
}
