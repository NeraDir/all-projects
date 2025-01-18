using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class pychka : MonoBehaviour
{
    // Start is called before the first frame update
    public float time_now = 0;
    public float speed = 1;
    public float damage = 1;
    public int hp = 3;
    public int level=1;
    public int x;
    public int y;

    void Start()
    {

        StreamReader wr = new StreamReader("Assets/Resources/"+gameObject.name +".txt");
        int q=int.Parse(wr.ReadLine());
        wr.Close();
        for (int i = 1; i < q; i++)
        {
            up();
        }
        level = 1;
    }
    public void up()
    {
        int type = -1;
        if (gameObject.name == "¿¬“ŒÃ¿“ 1")
            type = 0;
        else if (gameObject.name == "arbalet")
            type = 1;
        else if (gameObject.name == "¿¬“ŒÃ¿“ 3")
            type = 2;
        if (type == 0)
        {
            if (speed > 0.2f)
                speed -= 0.05f;
            damage += 0.25f;

            //func.create(gg.gameObject, x, y);
        }
        else if (type == 1)
        {
            if (speed > 0.5f)
                speed -= 0.025f;
            damage += 0.5f;
        }
        else
        {
            if (speed > 1)
                speed -= 0.5f;
            damage++;
        }

        hp++;
        level++;
    }
    // Update is called once per frame

    void FixedUpdate()
    {
        //Debug.Log(gameObject.GetInstanceID());
        if (gameObject.GetInstanceID() < 0 && (GameObject.Find("skelet"+x)!=null || (GameObject.Find("skelet gigant" + x) != null  && GameObject.Find("skelet gigant" + x).transform.position.z<120)))
        {
            time_now += Time.fixedDeltaTime;
            if (time_now >= speed)
            {
                GameObject gm = null;
                if (gameObject.name == "arbalet")
                {
                    gm = Instantiate(GameObject.Find("—“–≈À¿"));
                }
                else
                {
                    gm = Instantiate(GameObject.Find("œ”Àﬂ"));
                }
                Vector3 v3 = transform.position;
                v3.y = 8;
                gm.transform.position = v3;
                gm.GetComponent<pula>().damage = damage;
                time_now = 0;
            }
        }
        
    }
}
