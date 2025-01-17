using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static readonly Dictionary<string, IService> services = new Dictionary<string, IService>();

    public static void Register<T>(T service) where T : IService
    {
        string key = typeof(T).Name;
        if(services.ContainsKey(key))
        {
            Debug.LogError("RegError");
            return;
        }

        services.Add(key, service);
    }

    public static void Unregister<T>() where T : IService
    {
        string key = typeof(T).Name;
        if(!services.ContainsKey(key))
        {
            Debug.LogError("UnregError");
            return;
        }

        services.Remove(key);
    }

    public static T Get<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!services.ContainsKey(key))
        {
            Debug.LogError("GetError");
        }

        return (T)services[key];
    }

    private void OnDestroy()
    {
        services.Clear();
    }
}
