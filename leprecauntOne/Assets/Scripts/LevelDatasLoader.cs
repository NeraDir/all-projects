using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDatasLoader : MonoBehaviour
{
    [SerializeField]
    private List<LevelData> _levelDatas = new List<LevelData>();
    public static List<LevelData> LevelDatas = new List<LevelData>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadQuetions();
        
    }
    private void LoadQuetions()
    {
        TextAsset tempQuetionsTxt = Resources.Load<TextAsset>("Quetions");
        string[] alltxt = tempQuetionsTxt.text.Split("#");
        foreach (var item in alltxt)
        {
            LevelData tempData = new LevelData();
            string[] temp = item.Split("$");
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == "" || temp[i] == "")
                {
                    continue;
                }
                if (i == 0)
                    tempData.Quetion = temp[0].ToString();
                else
                    tempData.Answers.Add(temp[i].ToString());
            }
            _levelDatas.Add(tempData);
        }
        _levelDatas.Remove(_levelDatas[0]);
        LevelDatas = _levelDatas;
    }
}
