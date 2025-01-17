using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Text _coinText;
    public GameObject backgroundObject;
    public static bool endGame = false;
    public static bool hasTriggered = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Enemy"))
        {
            hasTriggered = true;
            Destroy(other.gameObject);
            CollisionController.Coin += CollisionController.currentCoin;
            _coinText.text = "+ " + CollisionController.currentCoin.ToString();
            endGame = true;
            SpawnEnemy._counterRound = 1;
            backgroundObject.SetActive(true);
        }
    }
}
