using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ui_button : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    public func func;
    public int x;
    public int y;
    public int type;
    string st_type;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
            if (gameObject.name == "bt")
            {

                if (gameObject.transform.parent.parent.parent.gameObject.name == "Image buy")
                {
                    st_type = "buy";
                }
                else if (gameObject.transform.parent.parent.gameObject.name == "Image up")
                {
                    st_type = "up";
                }

                if (st_type == "buy")
                {

                    Debug.Log("aaaaaaaaaa ");
                    if (func.money >= 100)
                    {
                        if (type == 0)
                        {
                            GameObject gg = func.lt_guns[0];
                            func.create(gg, x, y);
                        }
                        else if (type == 1)
                        {
                            GameObject gg = func.lt_guns[1];
                            func.create(gg, x, y);
                        }
                        else
                        {
                            GameObject gg = func.lt_guns[2];
                            func.create(gg, x, y);
                        }
                        func.money -= 100;
                    }

                }
                else if (st_type == "up")
                {
                    Debug.Log("x " + x + "       y " + y + "        ");
                    Debug.Log(" t " + func.p_mas[x, y].name);
                    if (func.money >= func.p_mas[x, y].GetComponent<pychka>().level * 200)
                    {
                        func.money -= func.p_mas[x, y].GetComponent<pychka>().level * 200;
                        func.p_mas[x, y].GetComponent<pychka>().up();
                        /*
                        if (func.p_mas[x, y].name == "юбрнлюр 1")
                            type = 0;
                        else if (func.p_mas[x, y].name == "arbalet")
                            type = 1;
                        if (func.p_mas[x, y].name == "юбрнлюр 3")
                            type = 2;
                        if (type == 0)
                        {
                            pychka gg = func.p_mas[x, y].GetComponent<pychka>();
                            if(gg.speed>0.2f)
                                gg.speed -= 0.05f;
                            gg.damage += 0.25f;

                            //func.create(gg.gameObject, x, y);
                        }
                        else if (type == 1)
                        {
                            pychka gg = func.p_mas[x, y].GetComponent<pychka>();
                            if (gg.speed > 0.2f)
                                gg.speed -= 0.075f;
                            gg.damage += 0.5f;
                        }
                        else
                        {
                            pychka gg = func.p_mas[x, y].GetComponent<pychka>();
                            if (gg.speed > 0.2f)
                                gg.speed -= 0.5f;
                            gg.damage ++;
                        }

                        func.p_mas[x, y].GetComponent<pychka>().hp++;
                        func.p_mas[x, y].GetComponent<pychka>().level++;
                        */

                    }
                }
                //GameObject.Find("platform"+(x+4*y))
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
            }
    }
    public void play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("play");
        //st_type = "up";
    }
    public void back()
    {
        SceneManager.LoadScene("menu");
        //st_type = "up";
    }
    public void upgrade()
    {
        SceneManager.LoadScene("upgrade");
        //st_type = "buy";
    }
    public void upper()
    {
        int gems = 0;
        int gems_now = 0;
        
        StreamReader wr = new StreamReader("Assets/Resources/gems.txt");
        gems = int.Parse(wr.ReadLine());
        wr.Close();
        string st = null;
        if (type == 0)
        {
            st= "Assets/Resources/юбрнлюр 1.txt";
        }
        else if (type == 1)
        {
            st= "Assets/Resources/arbalet.txt";
        }
        else if (type == 2)
        {
            st = "Assets/Resources/юбрнлюр 3.txt";
        }
        StreamReader wr1 = new StreamReader(st);
        gems_now = int.Parse(wr1.ReadLine());
        wr1.Close();
        gems_now = gems_now * 100;
        if (gems >= gems_now)
        {
            gems -= gems_now;
            gems_now = gems_now / 100 +1;
            StreamWriter sw = new StreamWriter("Assets/Resources/gems.txt");
            StreamWriter sw1 = new StreamWriter(st);
            sw.WriteLine(gems.ToString());
            sw1.WriteLine(gems_now.ToString());
            sw.Close();
            sw1.Close();
            GameObject.Find(type.ToString()).GetComponent<tx_up>().update_tx();
            GameObject.Find(type.ToString()+"m").GetComponent<tx_up>().update_tx();
            GameObject.Find("tx gems").GetComponent<tx_up>().update_tx();
        }
        
    }
    public void exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
        //st_type = "buy";
    }
    void Start()
    {
        if(gameObject.name!= "Button")
            func = GameObject.Find("func").GetComponent<func>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
