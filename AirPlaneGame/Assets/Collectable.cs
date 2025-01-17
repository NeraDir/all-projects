using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private bool heal = false;
    [SerializeField] private bool dd = false;
    [SerializeField] private bool money = false;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            if (heal)
            {
                player.Heal();
            }
            if (dd)
            {
                player.Attack();
            }
            if (money)
            {
                player.GetMoney();
            }
            Destroy(gameObject);
        }
    }

    private void Update() 
    {
        transform.position += new Vector3(0, -1.5f*Time.deltaTime, 0);
    }
}
