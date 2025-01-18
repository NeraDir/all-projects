using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteComponent : MonoBehaviour
{
    private Sequence sequence;

    private AudioClip clip;

    public GameObject getUpEffect;

    private void Start()
    {
        transform.localScale /= 2;
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(transform.localScale / 1.4f, 1.5f));
        sequence.Append(transform.DOScale(transform.localScale * 1.4f, 1.5f));
        sequence.SetLoops(-1, LoopType.Yoyo);
        clip = FindObjectOfType<GameController>().GetRandomClip();
    }

    public void Use() 
    {
        sequence.Kill();
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            GameController.currentFindNotes += 1;
            FindObjectOfType<GameController>().AudioSource.PlayOneShot(clip);
            GameController.time += 10.5f;
            Instantiate(getUpEffect,transform.position,transform.rotation);
            Destroy(gameObject);
        });
    }
}
