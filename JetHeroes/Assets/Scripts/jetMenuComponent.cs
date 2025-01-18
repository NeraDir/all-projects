using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jetMenuComponent : MonoBehaviour
{
    public GameObject jetHowToPlayWindow;

    public Transform jetCharacter;

    public TMP_Text jetScoreDisplay;

    public ParticleSystem jetParticleSystem;

    Sequence sequence;
    private void Start()
    {
        float yUpValue = jetCharacter.transform.position.y + 5;
        float yDownValue = jetCharacter.transform.position.y - 5;
        jetParticleSystem.startLifetime = 0.19f;
        sequence = DOTween.Sequence();
        sequence.Append(jetCharacter.DOMoveY(yUpValue, 1));
        sequence.Append(jetCharacter.DOMoveY(yDownValue, 1));
        sequence.Append(jetCharacter.DOMoveY(yUpValue, 1));
        sequence.SetLoops(-1, LoopType.Yoyo);

        if (!PlayerPrefs.HasKey("JetPlayerFirstEnterSavingValue"))
        {
            jetHowToPlayWindow.SetActive(true);
            PlayerPrefs.SetInt("JetPlayerFirstEnterSavingValue", 1);
        }
        jetScoreDisplay.text = jetGameComponent.jetBestScoreValue.ToString("0");
    }

    public void OnClickOpenGame() 
    {
        sequence.Kill();
        jetParticleSystem.startLifetime = 0.56f;
        Invoke("LaunchGame", 0.4f);
    }

    private void LaunchGame() 
    {
        jetCharacter.DOMoveY(jetCharacter.position.y + 100, 1).OnComplete(() => SceneManager.LoadScene("gameJet"));
    }

    public void OnClickCloseGame() 
    {
        Application.Quit();
    }
}
