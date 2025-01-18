using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textScoreToDie;
    [SerializeField] public float speedPlayer = 0.1f;

    [SerializeField] public bool isPlaying = true;

    void Start()
    {
        textScoreToDie.text = "Score to Die = "+GameManager.scoreToDie;
    }



    private void FixedUpdate()
    {

        if (Input.touchCount > 0 && isPlaying)
        {
            var posPlayerWorld = Camera.main.WorldToScreenPoint(transform.position);

            var p = Input.GetTouch(0).position - new Vector2(posPlayerWorld.x, posPlayerWorld.y);


            if (p.x > 0)
            {
                speedPlayer = 0.1f;
                MoveRight();
                transform.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                speedPlayer = -0.1f;
                MoveLeft();
                transform.GetComponent<SpriteRenderer>().flipX = false;
            }

        }
        else
        {
            speedPlayer = 0;

            if(GameManager.scoreToDie <= 0)
            {
                transform.Translate(0, 3f * Time.deltaTime, 0);
                transform.localScale -= new Vector3(0.001f,0.001f);
            }
        }


    }

    void MoveRight()
    {
        if (transform.position.x < 1.5f)
        {
            transform.Translate(new Vector3(speedPlayer, 0, 0));
        }
    }

    void MoveLeft()
    {
        if (transform.position.x > -1.5f)
        {
            transform.Translate(new Vector3(speedPlayer, 0, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlaying)
        {
            Destroy(collision.gameObject);

            GameManager.scoreToDie--;

            if (GameManager.scoreToDie <= 0)
            {
                isPlaying = false;

            }

            textScoreToDie.text = "Score to Die = " + GameManager.scoreToDie;
        }


    }
}
