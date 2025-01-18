using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pula : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public float speed =0.2f;
    public float timer;
    public float timer_now;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<mob>() != null)
        {
            col.gameObject.GetComponent<mob>().hp -= damage;
            
            if (col.gameObject.GetComponent<mob>().hp <= 0)
            {
                col.gameObject.GetComponent<BoxCollider>().enabled = false;
                //Destroy(col.gameObject);
                col.gameObject.GetComponent<Animator>().Play("dead");
                string[] h = col.gameObject.name.Split(" ");
                if (h.Length==1)
                {
                    GameObject.Find("func").GetComponent<func>().money += 10;
                    GameObject.Find("func").GetComponent<func>().gems_now++;
                }
                else
                {
                    GameObject.Find("func").GetComponent<func>().money += 50;
                    GameObject.Find("func").GetComponent<func>().gems_now+=5;
                }
                
            }
            Destroy(gameObject);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (gameObject.GetInstanceID() < 0)
        {
            timer_now += Time.fixedDeltaTime;
            if (timer_now >= timer)
            {
                Vector3 v3 = transform.position;
                v3.z += 0.5f;
                transform.position = v3;
                timer_now = 0;
                if (v3.z >= 250)
                {
                    Destroy(gameObject);
                }
            }
        }

    }
}
