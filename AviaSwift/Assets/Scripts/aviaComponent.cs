using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aviaComponent : MonoBehaviour
{
    public bool enemies;

    public int indexOfAviator;

    public Sprite[] aviatorsSprites;

    public int[] indexOfAvics;

    public SpriteRenderer aviatorSpriteRenderer;

    public int indexOfSprite;

    public bool isBullet;

    public Animator animator;

    public GameObject DestroyerEffect;

    public static int destroyedCountEnemies;

    public TrailRenderer trailRenderer;

    private void Start()
    {
        if (isBullet)
            return;
        indexOfSprite = Random.Range(0,aviatorsSprites.Length);
        aviatorSpriteRenderer = GetComponent<SpriteRenderer>();
        indexOfAviator = indexOfSprite;
    }

    private void LateUpdate()
    {
        if (enemies)
        {
            transform.position += new Vector3(0, -1, 0) * aviGameController.aviatorSpeed * Time.deltaTime;
            aviatorSpriteRenderer.sprite = aviatorsSprites[indexOfSprite];
        }
        else
        {
            if (isBullet)
            {
                transform.position += new Vector3(0, 1, 0) * (aviGameController.aviatorSpeed * aviGameController.multiplayMovementBullet) * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(0, 1, 0) * aviGameController.aviatorSpeed * Time.deltaTime;
                aviatorSpriteRenderer.sprite = aviatorsSprites[indexOfSprite];
            }
        }
    }

    public void DestroyMe() 
    {
        if (isBullet)
        {
            Destroy(gameObject);
        }
        else
        {
            animator.enabled = true;
            Invoke(nameof(SpawnEffect), 0.45f);
            Destroy(gameObject, 0.5f);
        }
    }

    private void SpawnEffect() 
    {
        Instantiate(DestroyerEffect, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out aviaComponent aviatorEnemie) && isBullet)
        {
            if ((indexOfSprite == 0) && (aviatorEnemie.indexOfSprite == 1))
            {
                destroyedCountEnemies++;
                if (destroyedCountEnemies >= 5)
                {
                    aviaEnemie.enemieHealth--;
                    destroyedCountEnemies = 0;
                }
                aviatorEnemie.DestroyMe();
            }
            else if ((indexOfSprite == 1) && (aviatorEnemie.indexOfSprite == 2))
            {
                destroyedCountEnemies++;
                if (destroyedCountEnemies >= 5)
                {
                    aviaEnemie.enemieHealth--;
                    destroyedCountEnemies = 0;
                }
                aviatorEnemie.DestroyMe();
            }
            else if ((indexOfSprite == 2) && (aviatorEnemie.indexOfSprite == 0))
            {
                destroyedCountEnemies++;
                if (destroyedCountEnemies >= 5)
                {
                    aviaEnemie.enemieHealth--;
                    destroyedCountEnemies = 0;
                }
                aviatorEnemie.DestroyMe();
            }
            else if (indexOfSprite  == aviatorEnemie.indexOfSprite)
            {
                aviatorEnemie.DestroyMe();
                Destroy(gameObject);
            }
            if (isBullet)
            {
                Destroy(gameObject);
            }
        }
        else if (other.TryGetComponent(out enemieHealth enemie) && !enemies && !isBullet)
        {
            aviaEnemie.enemieHealth -= 1;
            Destroy(gameObject);
        }
        else if (other.TryGetComponent(out playerHeamthl player) && enemies)
        {
            GameManager.PlayerHealth -= 1;
            Destroy(gameObject);
        }
    }
}
