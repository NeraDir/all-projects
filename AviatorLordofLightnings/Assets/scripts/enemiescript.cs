using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemiescript : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _enemieSprites;

    private Image _enemieImages;

    private int _enemieDamage;

    private void Start()
    {
        _enemieImages = GetComponent<Image>();
        int rndEnemie = Random.Range(0, _enemieSprites.Length);
        _enemieImages.sprite = _enemieSprites[rndEnemie];
        _enemieDamage = rndEnemie > 3 ? 3 : rndEnemie;
        Destroy(gameObject, 5);
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(-1, 0, 0) * 5 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out planescript plane))
        {
            gamecontrollerscript.heartsCount--;
            Destroy(gameObject);
        }
    }
}
