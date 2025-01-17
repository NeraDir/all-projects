using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aviPlayerController : MonoBehaviour
{
    public Color[] bulletColors;

    public Transform[] spawnBulletsPositions;

    public aviaComponent bullet;

    public Sprite[] sprites;

    public SpriteRenderer spriteRenderer;

    private Vector3 defaulScale = new Vector3(0.3308218f, 0.3308218f, 0.3308218f);

    private Vector3 maxScale = new Vector3(0.4f, 0.4f, 0.4f);

    private Vector3 minSacel = Vector3.zero;

    public int indexOfColor;

    private Rigidbody bodys;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }

    private void Start()
    {
        transform.localScale = maxScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        bodys = GetComponent<Rigidbody>();
        ChangeAviaPlane();
    }

    public void ChangeAviaPlane() 
    {
        StopAllCoroutines();
        StartCoroutine(ChangingColor());
    }

    private void LateUpdate()
    {
        if (DoubleClick())
        {
            ChangeAviaPlane();
        }

        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        bodys.velocity = new Vector3(direction.x,direction.y,0) * 5;
    }

    private IEnumerator LaunchAutoShooter() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(aviGameController.spawnBulletsTime);
            foreach (var item in spawnBulletsPositions)
            {
                aviaComponent bulleter = Instantiate(bullet, item.transform.position, item.rotation);
                bulleter.indexOfSprite = indexOfColor;
                bulleter.indexOfAviator = bulleter.indexOfSprite;
                bulleter.aviatorSpriteRenderer.color = bulletColors[bulleter.indexOfAviator];
                bulleter.trailRenderer.startColor = bulletColors[bulleter.indexOfAviator];
            }
        }
    }

    private IEnumerator ChangingColor()
    {
        while (transform.localScale != minSacel)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, minSacel, 10 * Time.deltaTime);
            yield return null;
        }
        indexOfColor += 1;
        if (indexOfColor >= sprites.Length)
        {
            indexOfColor = 0;
        }
        spriteRenderer.sprite = sprites[indexOfColor];
        StartCoroutine(Bigger());
    }

    private IEnumerator Bigger() 
    {
        while (transform.localScale != maxScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, maxScale, 10 * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(DefaultSize());
    }

    private IEnumerator DefaultSize() 
    {
        while (transform.localScale != defaulScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, defaulScale, 10 * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(LaunchAutoShooter());
    }
}
