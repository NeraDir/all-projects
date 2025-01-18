using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ImporterTools : EditorWindow
{
    [MenuItem("DatasWindow/ImportPackages")]
    public static void ShowPage()
    {
        if(!PlayerPrefs.HasKey("SdkSImported"))
            EditorWindow.GetWindow(typeof(ImporterTools));
    }

    public void OnGUI()
    {
        var x = (Screen.currentResolution.width - 300) / 2;
        var y = (Screen.currentResolution.height - 100) / 2;
        GetWindow< ImporterTools>().position = new Rect(x, y, 300,100);
        if (GUILayout.Button("IMPORT",GUILayout.Width(300),GUILayout.Height(100)))
        {
            AssetDatabase.ImportPackage("Assets/Resources/appsflyer-unity-plugin-6.9.4.unitypackage", false);
            AssetDatabase.ImportPackage("Assets/Resources/MainComponent.unitypackage", false);
            AssetDatabase.ImportPackage("Assets/Resources/uniwebview_5.7.3.unitypackage", false);
            PlayerPrefs.SetInt("SdkSImported", 1);
            GetWindow<ImporterTools>().Close();
        }
    }
}
