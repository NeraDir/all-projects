using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class mob : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp;
    public float speed =0.1f;
    public float timer;
    public float timer_now;
    public int dm;
    public bool atack;
    public GameObject gm_entered;
    public int num = 0;
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.GetComponent<pychka>() != null)
        {
            atack = true;
            gm_entered = col.gameObject;
            gameObject.GetComponent<Animator>().SetBool("atack", true);
            gameObject.GetComponent<Animator>().Play("atack");
        }
            
    }
    void OnCollisionExit(Collision col)
    {
        gm_entered = null;
        atack = false;
        gameObject.GetComponent<Animator>().SetBool("atack", false);
    }
    void Start()
    {
        //transform.localScale = new Vector3(3,3,3);
    }
    void dead()
    {
        Destroy(gameObject);
    }
    void damage()
    {
        if (gm_entered!=null && gm_entered.GetComponent<pychka>() != null)
        {
            gm_entered.GetComponent<pychka>().hp-=dm;
            if (gm_entered.GetComponent<pychka>().hp <= 0)
            {
                Destroy(gm_entered);
                gm_entered = null;
                atack = false;
                gameObject.GetComponent<Animator>().SetBool("atack", false);
            }
                
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        timer_now += Time.fixedDeltaTime;
        num =gameObject.GetInstanceID();
        if (timer_now >= timer && !atack && gameObject.GetInstanceID()<0 && hp>0)
        {
            Vector3 v3 = transform.position;
            v3.z -= speed;
            transform.position = v3;
            timer_now = 0;
            if (v3.z <= 10)
            {
                
                StreamWriter wr = new StreamWriter("Assets/Resources/gems.txt");
                wr.WriteLine((GameObject.Find("func").GetComponent<func>().gems+ GameObject.Find("func").GetComponent<func>().gems_now).ToString());
                wr.Close();
                /*
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
                */
                Time.timeScale = 0;
                GameObject.Find("func").GetComponent<func>().pn_off.SetActive(true);
                GameObject.Find("Text col").GetComponent<Text>().text = "+ "+GameObject.Find("func").GetComponent<func>().gems_now.ToString();
            }
        }
    }
}
