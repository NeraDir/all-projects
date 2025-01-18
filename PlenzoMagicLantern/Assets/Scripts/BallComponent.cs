using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;

public class BallComponent : MonoBehaviour,IPointerClickHandler
{
    [HideInInspector] public AudioSource audioSource;

    [HideInInspector]  public AudioClip clip;

    private bool clicked;

    private Vector3 beginScale;

    private Vector3 endScale = new Vector3(0,0,0);

    private Vector3 needScale;

    [HideInInspector] public int index;

    private void Start()
    {
        beginScale = transform.localScale;
        needScale = beginScale * 1.5f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clicked)
            return;
        if(GameManager.needIndex != index) 
        {
            GameManager.CurrentScore -= 25;
            return;
        }
            
        clicked = true;
        audioSource.PlayOneShot(clip);
        GameManager.CurrentScore += 50;
        StartCoroutine(Animations());
    }

    private IEnumerator Animations() 
    {
        while (transform.localScale != needScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, needScale, 10 * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(SecondPartAnimation());
    }

    private IEnumerator SecondPartAnimation()
    {
        while (transform.localScale != endScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, endScale, 20 * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
