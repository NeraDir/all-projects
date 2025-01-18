using UnityEngine;

public class EveliningAddManager : MonoBehaviour
{
    [HideInInspector] public string eveliningKey;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
