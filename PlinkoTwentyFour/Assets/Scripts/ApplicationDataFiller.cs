using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ApplicationDataFiller
{

    private string _applicationName;
    private string _applicationId;
    private string _applicationStoreId;
    private string _appsflyerKey;
    private Texture2D _applicationIcone;

    private string _appsPrefLink = "Assets/AppsFlyer/AppsFlyerObject.prefab";
    private string _appsScriptLink = "Assets/AppsFlyer/AppsFlyerObjectScript.cs";
    private string _contextScriptLink = "Assets/Samples/iOS 14 Advertising Support/1.0.0/01 Context Screen/Scripts/ContextScreenManager.cs";
    private string _posBuildStepLink = "Assets/Samples/iOS 14 Advertising Support/1.0.0/01 Context Screen/Scripts/Editor/PostBuildStep.cs";

    private AppsFlyerObjectScript _appsflyerScript;
    private List<Texture2D> _applicationIcon = new List<Texture2D>();
    private string _applicationSimpleBuildNumber;
    private string _applicationMainBuildNumber;

    public ApplicationDataFiller(string appName, string appId, string storeId, string devKey, Texture2D appIcon, string appLink, string checkKey)
    {
        _applicationName = appName;
        _applicationId = appId;
        _applicationStoreId = storeId;
        _appsflyerKey = devKey;
        _applicationIcone = appIcon;
        templink = appLink;
        mainKey = checkKey;
        OnSetFullData();
    }

    [ContextMenu("Fill Data")]
    private void OnSetData()
    {
        _applicationIcon.Add(_applicationIcone);
        Texture2D[] tempIcons = _applicationIcon.ToArray();
        _appsflyerScript = AssetDatabase.LoadAssetAtPath<AppsFlyerObjectScript>(_appsPrefLink);
        _appsflyerScript.appID = _applicationStoreId;
        _appsflyerScript.devKey = _appsflyerKey;
        _appsflyerScript.getConversionData = true;
        int temp_applicationMainBuildNumber = Random.Range(1, 10);
        float temp_applicationSimpleBuildNumber = Random.Range(1f, 10.5f);
        _applicationMainBuildNumber = temp_applicationMainBuildNumber.ToString();
        _applicationSimpleBuildNumber = temp_applicationSimpleBuildNumber.ToString("0.00");
        _applicationSimpleBuildNumber = _applicationSimpleBuildNumber.Replace(",", ".");
        string[] keys = _applicationId.Split('.');
        PlayerSettings.productName = _applicationName;
        PlayerSettings.bundleVersion = _applicationSimpleBuildNumber;
        PlayerSettings.companyName = keys[2];
        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, tempIcons);
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, _applicationId);
        PlayerSettings.iOS.buildNumber = _applicationMainBuildNumber;
        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
        PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneOnly;
    }

    [Header("Link")]
    public List<string> mathpantherString = new List<string>();
    public string templink;

    private string yammyGenerateTxt()
    {
        string tempTestTXT = "";
        foreach (var item in mathpantherString)
        {
            tempTestTXT += item;
        }
        return tempTestTXT;
    }

    [ContextMenu("Generate Rnd Link")]
    public void SetTxt()
    {
        GameObject TempMainManager = new GameObject($"{_applicationName.Replace(" ", "")}MainManager");
        mathpantherString.Clear();
        List<string> tempd = new List<string>();
        foreach (var item in templink)
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
            mathpantherString.Add(item.ToString());
        }

        Debug.Log(yammyGenerateTxt());
    }

    public string[] GetReadyString()
    {
        return mathpantherString.ToArray();
    }

    [ContextMenu("Generate Store Link")]
    private void OnGenerate()
    {
        string tempName = _applicationName.ToLower();
        tempName = tempName.Replace(" ", "-");
        tempName = tempName.Replace(":", "");
        string totalUrl = $"https://apps.apple.com/us/app/{tempName}/id{_applicationStoreId}";
        Debug.LogError(totalUrl);
        Application.OpenURL(totalUrl);
    }

    [ContextMenu("SetFullData")]
    private void OnSetFullData()
    {
        OnSetData();
        CreateNewTextFile();
        RewriteScripts();
        SetTxt();
        OnGenerate();
    }


    private string mainKey;

    private string[] inputData;

    private string[] addIndex;

    [ContextMenu("CreateNewScript")]
    private void CreateNewTextFile()
    {
        List<string> addIndexes = new List<string>();
        TextAsset[] tempAssets = Resources.LoadAll<TextAsset>("Patterns");
        foreach (var asset in tempAssets)
        {
            addIndexes.Add(asset.text);
        }
        inputData = addIndexes.ToArray();
        addIndexes.Clear();
        addIndexes.Add("MainManager");
        addIndexes.Add("LoadingManager");
        addIndex = addIndexes.ToArray();
        for (int i = 0; i < addIndex.Length; i++)
        {
            using (StreamWriter sw = File.CreateText("Assets/Scripts" + $"/{_applicationName.Replace(" ", "")}{addIndex[i]}.cs"))
            {
                foreach (var item in inputData[i].Split("##"))
                {
                    if (item == " ")
                        addIndexes.Remove(item);
                    string tempKey = item.ToString();
                    tempKey = tempKey.Replace("scriptName", _applicationName.Replace(" ", "") + addIndex[i]);
                    tempKey = tempKey.Replace("change", _applicationName.Replace(" ", "") + addIndex[i] + "Manager");
                    tempKey = tempKey.Replace("OnChange", "On" + _applicationName.Replace(" ", "") + "Method");
                    tempKey = tempKey.Replace("idfo", "idfaInfo" + _applicationName.ToLower().Replace(" ", "") + "Key");
                    tempKey = tempKey.Replace("link", _applicationName.ToLower().Replace(" ", "") + "LoadString");
                    tempKey = tempKey.Replace("context", "contextInfo" + _applicationName.ToLower().Replace(" ", "") + "IdfaDataKey");
                    tempKey = tempKey.Replace("dataKey", _applicationName.ToLower().Replace(" ", "") + "gameDataKey");
                    tempKey = tempKey.Replace("OnInum", "Launch" + _applicationName.ToLower().Replace(" ", "") + "GameInitialization");
                    tempKey = tempKey.Replace("inumstatus", _applicationName.ToLower().Replace(" ", "") + "initalizationStatus");
                    tempKey = tempKey.Replace("mainkey", mainKey);
                    tempKey = tempKey.Replace("ChaLoadg", "Load" + _applicationName.ToLower().Replace(" ", "") + "GameScene");
                    tempKey = tempKey.Replace("LoadingController", _applicationName.Replace(" ", "") + addIndex[i + 1 > addIndex.Length - 1 ? i : i + 1]);
                    tempKey = tempKey.Replace("menuScene", _applicationName.Replace(" ", "") + "MenuScene");
                    tempKey = tempKey.Replace("loascene", _applicationName.Replace(" ", "") + "LoadingScene");
                    sw.Write(tempKey);
                }
            }
        }
        AssetDatabase.Refresh();

    }
    private string[] path;

    private string[] rewiteTxt;

    [ContextMenu("RewriteScripts")]
    private void RewriteScripts()
    {
        List<string> paths = new List<string>();
        TextAsset[] tempAssets = Resources.LoadAll<TextAsset>("ReWrites");
        foreach (var asset in tempAssets)
        {
            paths.Add(asset.text);
        }
        rewiteTxt = paths.ToArray();
        paths.Clear();
        paths.Add(_appsScriptLink);
        paths.Add(_contextScriptLink);
        paths.Add(_posBuildStepLink);
        path = paths.ToArray();
        var folder = Directory.CreateDirectory("Assets/Samples/iOS 14 Advertising Support/1.0.0/01 Context Screen/Scripts/Editor");
        using (StreamWriter sw = File.CreateText(_posBuildStepLink))
        {
            foreach (var item in paths[2].Split("##"))
            {
                string tempKey = item.ToString();
                tempKey = tempKey.Replace("scriptName", _applicationName.Replace(" ", ""));
                tempKey = tempKey.Replace("change", _applicationName.Replace(" ", "") + "Manager");
                tempKey = tempKey.Replace("OnChange", "On" + _applicationName.Replace(" ", "") + "Method");
                tempKey = tempKey.Replace("idfo", "idfaInfo" + _applicationName.ToLower().Replace(" ", "") + "Key");
                tempKey = tempKey.Replace("link", _applicationName.ToLower().Replace(" ", "") + "LoadString");
                tempKey = tempKey.Replace("context", "contextInfo" + _applicationName.ToLower().Replace(" ", "") + "IdfaDataKey");
                tempKey = tempKey.Replace("dataKey", _applicationName.ToLower().Replace(" ", "") + "gameDataKey");
                tempKey = tempKey.Replace("OnInum", "Launch" + _applicationName.ToLower().Replace(" ", "") + "GameInitialization");
                tempKey = tempKey.Replace("inumstatus", _applicationName.ToLower().Replace(" ", "") + "initalizationStatus");
                tempKey = tempKey.Replace("mainkey", mainKey);
                tempKey = tempKey.Replace("ChaLoadg", "Load" + _applicationName.ToLower().Replace(" ", "") + "GameScene");
                tempKey = tempKey.Replace("LoadingController", _applicationName.Replace(" ", ""));
                tempKey = tempKey.Replace("menuScene", _applicationName.Replace(" ", "") + "MenuScene");
                tempKey = tempKey.Replace("loascene", _applicationName.Replace(" ", "") + "LoadingScene");
                tempKey = tempKey.Replace("aPpNaMe", _applicationName);
                sw.Write(tempKey);
            }
        }
        for (int i = 0; i < path.Length; i++)
        {
            File.Delete(path[i]);
            using (StreamWriter sw = File.AppendText(path[i]))
            {
                foreach (var item in rewiteTxt[i].Split("##"))
                {
                    string tempKey = item.ToString();
                    tempKey = tempKey.Replace("scriptName", _applicationName.Replace(" ", ""));
                    tempKey = tempKey.Replace("change", _applicationName.Replace(" ", "") + "Manager");
                    tempKey = tempKey.Replace("OnChange", "On" + _applicationName.Replace(" ", "") + "Method");
                    tempKey = tempKey.Replace("idfo", "idfaInfo" + _applicationName.ToLower().Replace(" ", "") + "Key");
                    tempKey = tempKey.Replace("link", _applicationName.ToLower().Replace(" ", "") + "LoadString");
                    tempKey = tempKey.Replace("context", "contextInfo" + _applicationName.ToLower().Replace(" ", "") + "IdfaDataKey");
                    tempKey = tempKey.Replace("dataKey", _applicationName.ToLower().Replace(" ", "") + "gameDataKey");
                    tempKey = tempKey.Replace("OnInum", "Launch" + _applicationName.ToLower().Replace(" ", "") + "GameInitialization");
                    tempKey = tempKey.Replace("inumstatus", _applicationName.ToLower().Replace(" ", "") + "initalizationStatus");
                    tempKey = tempKey.Replace("mainkey", mainKey);
                    tempKey = tempKey.Replace("ChaLoadg", "Load" + _applicationName.ToLower().Replace(" ", "") + "GameScene");
                    tempKey = tempKey.Replace("LoadingController", _applicationName.Replace(" ", ""));
                    tempKey = tempKey.Replace("menuScene", _applicationName.Replace(" ", "") + "MenuScene");
                    tempKey = tempKey.Replace("loascene", _applicationName.Replace(" ", "") + "LoadingScene");
                    tempKey = tempKey.Replace("aPpNaMe", _applicationName);
                    sw.Write(tempKey);
                }
            }
        }
    }
}
