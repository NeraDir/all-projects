using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrystallComponent : MonoBehaviour
{
    private Transform jarTransform;

    private Image myImageSprite;

    public Sprite[] sprites;

    public int indexOfCrystall;

    private Animator anima;

    public float Damage;

    private bool destroy;

    private void Start()
    {
        Damage = RamGameManager.crystallDamage;
        anima = GetComponent<Animator>();
        indexOfCrystall = Random.Range(0, sprites.Length);
        myImageSprite = GetComponent<Image>();
        myImageSprite.sprite = sprites[indexOfCrystall];
        jarTransform = FindObjectOfType<JarComponent>().transform;
    }

    private void LateUpdate()
    {
        if (RamGameManager.GameEnded)
            return;
        if (destroy)
            return;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, jarTransform.localPosition, RamGameManager.crystallMovementSpeed * Time.deltaTime);
    }

    public void Destroye() 
    {
        destroy = true;
        anima.enabled = true;
        Destroy(gameObject,0.5f);
    }
}
