using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class tx_up : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        update_tx();
    }
    public void update_tx()
    {
        
        if (gameObject.name == "tx gems")
        {
            StreamReader wr = new StreamReader("Assets/Resources/gems.txt");
            gameObject.GetComponent<Text>().text = wr.ReadLine();
            wr.Close();
            //wr.WriteLine(GameObject.Find("func").GetComponent<func>().gems.ToString());
        }

        else if(gameObject.name == "0")
        {
            StreamReader wr = new StreamReader("Assets/Resources/юбрнлюр 1.txt");
            gameObject.GetComponent<Text>().text = (int.Parse( wr.ReadLine())*100).ToString();
            wr.Close();
        }
        else if (gameObject.name == "1")
        {
            StreamReader wr = new StreamReader("Assets/Resources/arbalet.txt");
            gameObject.GetComponent<Text>().text = (int.Parse(wr.ReadLine()) * 100).ToString();
            wr.Close();
        }
        else if (gameObject.name == "2")
        {
            StreamReader wr = new StreamReader("Assets/Resources/юбрнлюр 3.txt");
            gameObject.GetComponent<Text>().text = (int.Parse(wr.ReadLine()) * 100).ToString();
            wr.Close();
        }

        else if (gameObject.name == "0m")
        {
            StreamReader wr = new StreamReader("Assets/Resources/юбрнлюр 1.txt");
            gameObject.GetComponent<Text>().text = "lvl "+wr.ReadLine();
            wr.Close();
        }
        else if (gameObject.name == "1m")
        {
            StreamReader wr = new StreamReader("Assets/Resources/arbalet.txt");
            gameObject.GetComponent<Text>().text = "lvl " + wr.ReadLine();
            wr.Close();
        }
        else if (gameObject.name == "2m")
        {
            StreamReader wr = new StreamReader("Assets/Resources/юбрнлюр 3.txt");
            gameObject.GetComponent<Text>().text = "lvl " + wr.ReadLine();
            wr.Close();
        }
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
