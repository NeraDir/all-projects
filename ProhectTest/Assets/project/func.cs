using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class func : MonoBehaviour
{
    // Start is called before the first frame update
    public float time = 0;
    public float time_now = 0;
    public float time_global=0;
    public int sh = 0;

    public int money = 200;
    public int gems = 0;
    public int gems_now = 0;

    public GameObject[,] p_mas = new GameObject[4,2];
    public GameObject[,] platform = new GameObject[4,2];
    public List<GameObject> lt_guns = new List<GameObject>();

    public GameObject pn_off;

    void Start()
    {

        
        StreamReader rd = new StreamReader("Assets/Resources/gems.txt");
        gems = int.Parse(rd.ReadLine());
        rd.Close();
        
        GameObject gm1 = GameObject.Find("Image up");
        foreach (Transform tr in gm1.transform)
        {
             tr.gameObject.SetActive(false);
        }
        gm1 = GameObject.Find("Image buy");
        foreach (Transform tr in gm1.transform)
        {
            tr.gameObject.SetActive(false);
        }

        for (int i = 0; i < 8; i++)
        {
            platform[i % 4, i / 4] = GameObject.Find("platform" + i);
        }


    }
    public void create(GameObject gm,int x)
    {
        string nm = gm.name;
        gm = Instantiate(gm);
        if (gm.GetComponent<mob>() != null)
        {
            gm.transform.position = new Vector3(13 * (x - 1), 2, 135);
            gm.name = nm;
            gm.name +=x;
            
        }
        
    }
    public void create(GameObject gm, int x,int y)
    {
        string nm = gm.name;
        gm = Instantiate(gm);
        gm.name=nm;
        if (gm.GetComponent<pychka>() != null)
        {
            gm.GetComponent<pychka>().x = x;
            gm.GetComponent<pychka>().y = y;
            gm.transform.position = new Vector3(13 * (x - 1), 2, 30+13*y);
            platform[x, y].GetComponent<platforma>().gm = gm;
            p_mas[x, y] = gm;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log((p_mas[1, 1] == null) + "      u  ");
        time_now += Time.fixedDeltaTime;
        time_global += Time.fixedDeltaTime;
        if (time_now >= time)
        {
            if (sh < 15)
            {
                //if(time_global)
                if (time_global < 120 && Random.Range(0, 5) == 0)
                {
                    create(GameObject.Find("skelet"), Random.Range(0, 4));
                    sh++;
                }
                else if (Random.Range(0, 100) < (time_global / 6))
                {
                    create(GameObject.Find("skelet"), Random.Range(0, 4));
                    sh++;
                }

            }

            else
            {
                create(GameObject.Find("skelet gigant"), Random.Range(0, 4));
                sh = 0;
            }
            time_now = 0;
        }
        GameObject.Find("tx money").GetComponent<Text>().text = money.ToString();
    }
}
