using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class DataFillWindow : EditorWindow
{
    private string appName;
    private string appId;
    private string storeId;
    private string devKey;
    private Texture2D appIcon;
    private string link;
    private string checkKey;

    private List<int> readys = new List<int>();

    [MenuItem("Tools/DataFill")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(DataFillWindow));
    }

    private void OnGUI()
    {
        GUILayout.Label("Application Data", EditorStyles.boldLabel);
        appName = EditorGUILayout.TextField("App Name", appName);
        appId = EditorGUILayout.TextField("App Bundle ID", appId);
        storeId = EditorGUILayout.TextField("App Store ID", storeId);
        devKey = EditorGUILayout.TextField("App Dev Key", devKey);
        appIcon = (Texture2D)EditorGUILayout.ObjectField("App ICON", appIcon, typeof(Texture2D), false);
        link = EditorGUILayout.TextField("App Link", link);
        checkKey = EditorGUILayout.TextField("App Link Check Key", checkKey);
        GUILayout.Space(30);
        GUIStyle contentStyle = new GUIStyle();
        contentStyle.alignment = TextAnchor.MiddleLeft;
        if (GUILayout.Button("Import Appsflyer", GUILayout.Height(80)))
        {
            if (readys.Contains(1))
                return;
            AssetDatabase.ImportPackage("Assets/Resources/appsflyer-unity-plugin-6.9.4.unitypackage",false);
            AssetDatabase.ImportPackage("Assets/Resources/MainComponent.unitypackage", false);
            readys.Add(1);

        }
        if (GUILayout.Button("Import UniWebview", GUILayout.Height(80)))
        {
            if (readys.Contains(2))
                return;
            AssetDatabase.ImportPackage("Assets/Resources/uniwebview_5.7.3.unitypackage", false);
            readys.Add(2);
        }
        if (readys.Count >= 2)
        {
            if (GUILayout.Button("Fill", GUILayout.Height(80)))
            {

                if(ApplicationDataFiller != null)
                ApplicationDataFiller appFiller = new ApplicationDataFiller(appName, appId, storeId, devKey, appIcon, link, checkKey);
            }
        }
    }
}
