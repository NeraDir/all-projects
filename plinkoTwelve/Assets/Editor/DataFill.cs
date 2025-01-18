using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataFill : EditorWindow
{
    private float _width = 600;
    private float _height = 350;

    private string _appName;
    private string _appId;
    private string _appStoreId;
    private string _appDevKey;
    private string _appLink;
    private string _appCheckKey;
    private Texture2D _appIcon;

    public List<string> Strings = new List<string>();

    [MenuItem("DatasWindow/Fill")]
    public static void ShowPage()
    {
        if (!PlayerPrefs.HasKey("SdkSImported"))
            return;
        if (!PlayerPrefs.HasKey("DataFilled"))
            GetWindow(typeof(DataFill));
    }

    public void OnGUI()
    {
        var x = (Screen.currentResolution.width - _width) / 2;
        var y = (Screen.currentResolution.height - _height) / 2;
        GetWindow<DataFill>().position = new Rect(x, y, _width, _height);
        GUILayout.Space(5);
        GUILayout.Label("Application Data", EditorStyles.boldLabel);
        _appName = EditorGUILayout.TextField("NAME", _appName);
        _appId = EditorGUILayout.TextField("ID BUNDLE", _appId);
        _appStoreId = EditorGUILayout.TextField("STORE ID", _appStoreId);
        _appDevKey = EditorGUILayout.TextField("DEV KEY", _appDevKey);
        _appLink = EditorGUILayout.TextField("LINK", _appLink);
        _appCheckKey = EditorGUILayout.TextField("CHECK KEY", _appCheckKey);
        _appIcon = (Texture2D)EditorGUILayout.ObjectField("ICON", _appIcon, typeof(Texture2D), false);
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("Strings");

        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
        so.ApplyModifiedProperties(); // Remember to apply modified properties
        GUILayout.Space(10);
        if (GUILayout.Button("SET DATA", GUILayout.Width(_width), GUILayout.Height(_height/4)))
        {
            ApplicationDataFiller dataFiller = new ApplicationDataFiller(_appName, _appId, _appStoreId, _appDevKey, _appIcon, _appLink, _appCheckKey);
            PlayerPrefs.SetInt("DataFilled", 1);
            SetTxt();
        }
    }

    private string yammyGenerateTxt()
    {
        string tempTestTXT = "";
        foreach (var item in Strings)
        {
            tempTestTXT += item;
        }
        return tempTestTXT;
    }

    [ContextMenu("Generate Rnd Link")]
    public void SetTxt()
    {
        Strings.Clear();
        List<string> tempd = new List<string>();
        foreach (var item in _appLink)
        {
            tempd.Add(item.ToString());
        }
        for (int i = 0; i < tempd.Count; i++)
        {
            if (Random.Range(0, 2) != 0)
            {
                tempd.Insert(i, "#");
            }
        }
        string temp = "";
        foreach (var item in tempd)
        {
            temp += item;
        }
        string[] sdfgdsf = temp.Split("#");
        foreach (var item in sdfgdsf)
        {
            if (item == "")
            {
                continue;
            }
            Strings.Add(item.ToString());
        }

        Debug.Log(yammyGenerateTxt());
    }
}
