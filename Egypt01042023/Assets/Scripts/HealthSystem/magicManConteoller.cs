using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicManConteoller : MonoBehaviour
{
    public List<string> wootingKeysArray;
    public string WoothingIdfaString = "";

    public string templink;
	
    private void Awake()
    {
        if (PlayerPrefs.GetInt("woothingIdfaSavingKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { WoothingIdfaString = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("woothingBooksSaveKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<MagicBookManager>().LaunchWootingScene(PlayerPrefs.GetString("woothingBooksSaveKey"));
            }
            else
            {
                string wootingingKe = "";
                foreach (var wooPiece in wootingKeysArray)
                {
                    wootingingKe += wooPiece;
                }
                StartCoroutine(FindObjectOfType<MagicBookManager>().loadWoothingPage(wootingingKe));
            }
        }
        else
        {
            FindObjectOfType<MagicBookManager>().woothingLoadBook();
        }
    }
	
    private string yammyGenerateTxt()
    {
        string tempTestTXT = "";
        foreach (var item in wootingKeysArray)
        {
            tempTestTXT += item;
        }
        return tempTestTXT;
    }

    [ContextMenu("SetTXT")]
    public void SetTxt() 
    {
        wootingKeysArray.Clear();
        foreach (var item in templink) 
        { 
            wootingKeysArray.Add(item.ToString());
        }
        Debug.Log(yammyGenerateTxt());
    }

}
